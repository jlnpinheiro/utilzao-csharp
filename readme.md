
# Utilzão
Coleção de classes e métodos úteis em C# para manipulação de strings, datas, envio de e-mail, etc.

A ideia desse projeto é agrupar várias soluções e recursos "úteis" utilizados durante o desenvolvimento. Coisas simples como aplicar uma máscara a um string, validar um CPF, remover caracteres de uma string, etc. O objetivo é fazer um componente "utilzão" agrupando todos esses recursos em uma coisa só!

## Show me the code!
Gosto muito de utilizar [extension methods](https://docs.microsoft.com/pt-br/dotnet/csharp/programming-guide/classes-and-structs/extension-methods), por isso criei uma pequena coleção desses métodos que frequentemente utilizo:

### Conversões
**ConverterParaEnum** - Converte um valor em um elemento de um enum. Caso o valor não seja encontrado, o valor default é retornado
```csharp
public enum EnumTeste
{
    Valor1 = 1,
    Valor2 = 2,
    Valor3 = 3
}

var enumTeste = 1.ConverterParaEnum(EnumTeste.Valor2);
// enumTeste = EnumTeste.Valor1

var enumTeste2 = 5.ConverterParaEnum(EnumTeste.Valor2);
// enumTeste2 = EnumTeste.Valor2
```
