using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class DecryptFiles
{
    // Shared configuration
    private const string Password = "u8DurGE2";
    private const string Salt = "6BBGizHE";
    private const int IterationCount = 1000;
    private const int KeySizeBits = 128;
    private const int BlockSizeBits = 128;

    // Method to load and decrypt a file
    public static byte[] LoadAndDecrypt(string inPath)
    {
        byte[] array = File.ReadAllBytes(inPath);
        return DecryptBytes(array);
    }

    // Creates and configures the algorithm with derived Key/IV
    private static RijndaelManaged CreateConfiguredAlgorithm()
    {
        var algo = new RijndaelManaged
        {
            KeySize = KeySizeBits,
            BlockSize = BlockSizeBits,
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7
        };

        byte[] saltBytes = Encoding.UTF8.GetBytes(Salt);
        using (var derive = new Rfc2898DeriveBytes(Password, saltBytes, IterationCount))
        {
            algo.Key = derive.GetBytes(algo.KeySize / 8);
            algo.IV = derive.GetBytes(algo.BlockSize / 8);
        }

        return algo;
    }

    // Method to decrypt bytes
    public static byte[] DecryptBytes(byte[] array)
    {
        using (var algo = CreateConfiguredAlgorithm())
        using (ICryptoTransform decryptor = algo.CreateDecryptor())
        {
            return decryptor.TransformFinalBlock(array, 0, array.Length);
        }
    }

    // Method to encrypt bytes
    public static byte[] EncryptBytes(byte[] array)
    {
        using (var algo = CreateConfiguredAlgorithm())
        using (ICryptoTransform encryptor = algo.CreateEncryptor())
        {
            return encryptor.TransformFinalBlock(array, 0, array.Length);
        }
    }

    // Main method to traverse all files in a directory and process them (encrypt/decrypt)
    public static void ProcessFilesInDirectory(string mode, string inputDirectory, string outputDirectory)
    {
        try
        {
            if (!Directory.Exists(inputDirectory))
            {
                Console.WriteLine("Input directory does not exist: " + inputDirectory);
                return;
            }

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            string[] files = Directory.GetFiles(inputDirectory, "*.*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                try
                {
                    byte[] inputData = File.ReadAllBytes(file);
                    byte[] outputData = mode == "e" ? EncryptBytes(inputData) : DecryptBytes(inputData);

                    string relativePath = Path.GetRelativePath(inputDirectory, file);
                    string outputPath = Path.Combine(outputDirectory, relativePath);
                    string outputDir = Path.GetDirectoryName(outputPath) ?? outputDirectory;
                    if (!Directory.Exists(outputDir))
                    {
                        Directory.CreateDirectory(outputDir);
                    }

                    File.WriteAllBytes(outputPath, outputData);

                    Console.WriteLine((mode == "e" ? "File encrypted" : "File decrypted") + " and saved: " + outputPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error processing file " + file + ": " + ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error processing directory: " + ex.Message);
        }
    }

    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DecryptFiles <e|d> <input_directory> [output_directory]");
            Console.WriteLine("   e = encrypt, d = decrypt");
            return;
        }

        string mode = args[0].ToLower();
        if (mode != "e" && mode != "d")
        {
            Console.WriteLine("Invalid mode. Use 'e' to encrypt or 'd' to decrypt.");
            return;
        }

        string inputDirectory = args[1];
        string outputDirectory = args.Length >= 3 ? args[2] : inputDirectory;

        ProcessFilesInDirectory(mode, inputDirectory, outputDirectory);
    }
}
