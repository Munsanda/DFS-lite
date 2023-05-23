using DFS.DTOs;
using File = DFS.Models.File;

namespace DFS.Extentions
{
    public static class FileExtention
    {
        public static CreateFileDTO AsDto(this File file)
        {
            return new CreateFileDTO
            {
                file_name = file.file_name,
                file_content = file.file_content
            };
        }
    }
}
