using DotNet8._0_BatchFileWriter;

var filePath = ReadString("File path", required: true);

var processor = new BatchLineProcessor(filePath!);
var generator = new LineGenerator(new LineGeneratorBlockingCollectionOutput(processor.Collection), new LineGeneratorDefaultStrategy());

await Task.WhenAll(
    Task.Run(generator.GenerateAsync),
    Task.Run(processor.ProcessAsync)
);

static string? ReadString(string key, bool required = false)
{
    WriteReadKey(key);

    string? value = string.Empty;
    do
    {
        value = Console.ReadLine();
    } while (required && string.IsNullOrWhiteSpace(value));

    return value;
}

static void WriteReadKey(string key) => Console.Write($"{key}: ");