using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RFI.DuplicitFilesChecker.Services
{
    public class Class1
    {
        public void Tmp()
        {
            Console.WriteLine("Search pattern AllDirectories returns:");
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\tomfitz\Documents\ExampleDir");
            foreach (var fi in di.GetFiles("*", SearchOption.AllDirectories))
            {
                Console.WriteLine(fi.Name);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        static void Main(string[] args)
        {
            string source = "Hello World!";
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, source);

                Console.WriteLine("The MD5 hash of " + source + " is: " + hash + ".");

                Console.WriteLine("Verifying the hash...");

                if (VerifyMd5Hash(md5Hash, source, hash))
                {
                    Console.WriteLine("The hashes are the same.");
                }
                else
                {
                    Console.WriteLine("The hashes are not same.");
                }
            }
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////


        public static void Main2(String[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No directory selected.");
                return;
            }

            string directory = args[0];
            if (Directory.Exists(directory))
            {
                // Create a DirectoryInfo object representing the specified directory.
                var dir = new DirectoryInfo(directory);
                // Get the FileInfo objects for every file in the directory.
                FileInfo[] files = dir.GetFiles();
                // Initialize a SHA256 hash object.
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    // Compute and print the hash values for each file in directory.
                    foreach (FileInfo fInfo in files)
                    {
                        try
                        {
                            // Create a fileStream for the file.
                            FileStream fileStream = fInfo.Open(FileMode.Open);
                            // Be sure it's positioned to the beginning of the stream.
                            fileStream.Position = 0;
                            // Compute the hash of the fileStream.
                            byte[] hashValue = mySHA256.ComputeHash(fileStream);
                            // Write the name and hash value of the file to the console.
                            Console.Write($"{fInfo.Name}: ");
                            PrintByteArray(hashValue);
                            // Close the file.
                            fileStream.Close();
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine($"I/O Exception: {e.Message}");
                        }
                        catch (UnauthorizedAccessException e)
                        {
                            Console.WriteLine($"Access Exception: {e.Message}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("The directory specified could not be found.");
            }
        }

        // Display the byte array in a readable format.
        public static void PrintByteArray(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]:X2}");
                if ((i % 4) == 3) Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}
