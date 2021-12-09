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
