using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
class DecryptFiles
{
    // Método para cargar y desencriptar un archivo
    public static byte[] LoadAndDecrypt(string inPath)
    {
        byte[] array = File.ReadAllBytes(inPath);
        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.KeySize = 128;
        rijndaelManaged.BlockSize = 128;
        string password = "u8DurGE2";
        string s = "6BBGizHE";
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, bytes);
        rfc2898DeriveBytes.IterationCount = 1000;
        rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
        rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
        ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
        byte[] result = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
        cryptoTransform.Dispose();
        return result;
    }

    // Método principal para recorrer todos los archivos en un directorio y desencriptarlos
    public static void DecryptFilesInDirectory(string inputDirectory, string outputDirectory)
    {
        try
        {
            // Crear el directorio de salida si no existe
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            // Obtener todos los archivos en el directorio de entrada recursivamente
            string[] files = Directory.GetFiles(inputDirectory, "*.*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                try
                {

                    // Desencriptar el archivo
                    byte[] decryptedData = LoadAndDecrypt(file);

                    // Crear la estructura de subdirectorios en el directorio de salida
                    string relativePath = Path.GetRelativePath(inputDirectory, file);
                    string outputPath = Path.Combine(outputDirectory, relativePath);
                    string outputDir = Path.GetDirectoryName(outputPath) ?? inputDirectory;
                    if (!Directory.Exists(outputDir))
                    {
                        Directory.CreateDirectory(outputDir);
                    }

                    // Guardar los datos desencriptados en un nuevo archivo
                    File.WriteAllBytes(outputPath, decryptedData);

                    Console.WriteLine("Archivo desencriptado y guardado: " + outputPath);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al desencriptar el archivo " + file + ": " + ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al procesar el directorio: " + ex.Message);
        }
    }

    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Uso: DecryptFiles <directorio_entrada> <directorio_salida>");
            return;
        }

        string inputDirectory = args[0];
        string outputDirectory;
        if (args.Length < 2)
            outputDirectory = args[1];
        else
            outputDirectory = inputDirectory;

        DecryptFilesInDirectory(inputDirectory, outputDirectory);
    }
}
