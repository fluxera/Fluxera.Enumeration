# Fluxera.Enumeration
An object-oriented enumeration library.

## Usage

Define an enumerable as C# class that inherits from the ```Enumeration<TEnum>``` base class.
Add the enumeration options as public static readonly fields to the new enumerable class.

```c#
public sealed class Color : Enumeration<Color>
{
    public static readonly Color Red = new Color(0, "FF0000");
    public static readonly Color Green = new Color(1, "00FF00");
    public static readonly Color Blue = new Color(2, "0000FF");
    public static readonly Color White = new Color(3, "FFFFFF");
    public static readonly Color Black = new Color(4, "000000");

    /// <inheritdoc />
    private Color(int value, string hexValue, [CallerMemberName] string name = null!) 
        : base(value, name)
    {
        this.HexValue = hexValue;
    }

    public string HexValue { get; }
}
```

```c#
public class MessageType : Enumeration<MessageType>
{
    public static readonly MessageType Email = new EmailType();
    public static readonly MessageType Postal = new PostalType();
    public static readonly MessageType TextMessage  = new TextMessageType();

    /// <inheritdoc />
    private MessageType(int value, string name) 
        : base(value, name)
    {
    }

    private sealed class EmailType : MessageType
    {
        /// <inheritdoc />
        public EmailType() : base(0, "Email")
        {
        }
    }

    private sealed class PostalType : MessageType
    {
        /// <inheritdoc />
        public PostalType() : base(1, "Postal")
        {
        }
    }

    private sealed class TextMessageType : MessageType
    {
        /// <inheritdoc />
        public TextMessageType() : base(2, "TextMessage")
        {
        }
    }
}
```

```c#
public abstract class Animal : Enumeration<Animal>
{
    /// <inheritdoc />
    protected Animal(int value, string name) 
        : base(value, name)
    {
    }
}

public sealed class Mammal : Animal
{
    public static readonly Mammal Tiger = new Mammal(0);
    public static readonly Mammal Elephant = new Mammal(1);

    /// <inheritdoc />
    private Mammal(int value, [CallerMemberName] string name = null!) 
        : base(value, name)
    {
    }
}

public sealed class Reptile : Animal
{
    public static readonly Reptile Iguana = new Reptile(2);
    public static readonly Reptile Python = new Reptile(3);

    /// <inheritdoc />
    private Reptile(int value, [CallerMemberName] string name = null!) 
        : base(value, name)
    {
    }
}
```

The ```Enumeration<TEnum>``` provides a fluent API to configure switch-case like structures
to simplify the execution of action for specific cases.

```c#
public void PrintColorInfo(Color color)
{
    color
        .When(Color.Red).Then(() => SetConsoleColor(ConsoleColor.Red))
        .When(Color.Blue).Then(() => SetConsoleColor(ConsoleColor.Blue))
        .When(Color.Green).Then(() => SetConsoleColor(ConsoleColor.Green))
        .Default(() => SetConsoleColor(ConsoleColor.White));

    Console.WriteLine($"{color}({color.Value}) => #{color.HexValue}");
}
```

## Serialization Support

At the moment serialization support is available for:

- [Entity Framework Core](https://github.com/dotnet/efcore)
- [Newtonsoft.Json (JSON.NET)](https://github.com/JamesNK/Newtonsoft.Json)
- [LiteDB](https://github.com/mbdavid/LiteDB)
- [MongoDB](https://github.com/mongodb/mongo-csharp-driver)
- [System.Text.Json](https://github.com/dotnet/corefx/tree/master/src/System.Text.Json)

### Entity Framework Core

To support ```Enumeration<TEnum>``` in EFCore use **one** of the available extension methods on the ```ModelBuilder```.

```c#
// Store the name in the database.
modelBuilder.ApplyEnumerationNameConversions();

// Store the value in the database.
modelBuilder.ApplyEnumerationValueConversions();
```

### Newtonsoft.Json (JSON.NET)

To support ```Enumeration<TEnum>``` in JSON.NET use **one** of the available extensions methods on the ```JsonSerializerSettings```.

```c#
JsonSerializerSettings settings = new JsonSerializerSettings();

// Use the name to serialize all enumerations.
settings.UseEnumerationNameConverter();

// Use the value to serialize all enumerations.
settings.UseEnumerationValueConverter();

// Use the name to serialize just the Color enumeration.
settings.UseEnumerationNameConverter<Color>();

// Use the value to serialize just the Color enumeration.
settings.UseEnumerationValueConverter<Color>();

JsonConvert.DefaultSettings = () => settings;
```

### LiteDB

To support ```Enumeration<TEnum>``` in LiteDB use **one** of the available extensions methods on the ```BsonMapper```.

```c#
// Store the name in the database for all enumerations available in the given assembly.
BsonMapper.Global.UseEnumerationName(Assembly.GetExecutingAssembly());

// Store the value in the database for all enumerations available in the given assembly.
BsonMapper.Global.UseEnumerationValue(Assembly.GetExecutingAssembly());

// Store the name in the database just for the Color enumeration.
BsonMapper.Global.UseEnumerationName<Color>();

// Store the value in the database just for the Color enumeration.
BsonMapper.Global.UseEnumerationName<Color>();
```

### MongoDB

To support ```Enumeration<TEnum>``` in MongoDB use **one** of the available extensions methods on the ```ConventionPack```.

```c#
ConventionPack pack = new ConventionPack();

// Store the name in the database.
pack.AddEnumerationNameConvention();

// Store the value in the database.
pack.AddEnumerationValueConvention();

ConventionRegistry.Register("ConventionPack", pack, t => true);
```

### System.Text.Json

To support ```Enumeration<TEnum>``` in System.Text.Json use **one** of the available extensions methods on the ```JsonSerializerOptions```.

```c#
JsonSerializerOptions options = new JsonSerializerOptions();

// Use the name to serialize all enumerations.
options.UseEnumerationNameConverter();

// Use the value to serialize all enumerations.
options.UseEnumerationValueConverter();

// Use the name to serialize just the Color enumeration.
settings.UseEnumerationNameConverter<Color>();

// Use the value to serialize just the Color enumeration.
settings.UseEnumerationValueConverter<Color>();
```

## Future

We plan to implement support for OData server- and client-side to enable queries on ```Enumeration``` like is was an simple C# ```enum```. 
An ```Enumeration``` should generate an ```EdmEnumType``` in the metadata for an OData feed.

The simple way would be to add the enumeraions as complex type, but that feels wrong because
the object-oriented enumeration should work like an ```enum``` and the EDM model would not
reflect the available options.

```c#
EdmEnumType color = new EdmEnumType("Default", "Color");
color.AddMember(new EdmEnumMember(color, "Red", new EdmIntegerConstant(0)));
color.AddMember(new EdmEnumMember(color, "Blue", new EdmIntegerConstant(1)));
color.AddMember(new EdmEnumMember(color, "Green", new EdmIntegerConstant(2)));
model.AddElement(color);
```

This cannot be achieved using the model builder because the enum methods are tightly bound to C# ```enum```.

```xml
<EnumType Name="Color">
   <Member Name="Red" Value="0" />
   <Member Name="Blue" Value="1" />
   <Member Name="Green" Value="2" />
</EnumType>
```

We want to support filter queries like so:

```plaintext
https://localhost:5001/odata/Cars?$filter=Color eq 'Blue'
https://localhost:5001/odata/Cars?$filter=Color eq 1
```

https://github.com/OData/WebApi/issues/1186
https://github.com/OData/WebApi/blob/master/src/Microsoft.AspNet.OData.Shared/UnqualifiedCallAndEnumPrefixFreeResolver.cs

We also plan to support Swagger documentation in ASP.NET Core applications.

## References

This library was inspired by:

[Søren Pedersen](https://gist.github.com/spewu) - [Enumeration](https://gist.github.com/spewu/5933739)

[Steve Smith](https://github.com/ardalis) - [SmartEnum](https://github.com/ardalis/SmartEnum)

[Kyle Herzog](https://github.com/kyleherzog) - [ExtendableEnums](https://github.com/kyleherzog/ExtendableEnums)

[MakoLab S.A.](https://github.com/MakoLab) - [SpecializedEnum](https://github.com/MakoLab/fractus/blob/master/dotNet/CommunicationProjects/zzTest/SpecializedEnum.cs)
