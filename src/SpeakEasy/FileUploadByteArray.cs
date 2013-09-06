using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    internal class FileUploadByteArray : FileUpload
    {
        private readonly byte[] contents;

        public FileUploadByteArray(string name, string fileName, byte[] contents)
            : base(name, fileName)
        {
            this.contents = contents;
        }

        public override Task WriteToAsync(Stream stream)
        {
            return stream.WriteAsync(contents, 0, contents.Length);
        }
    }
}