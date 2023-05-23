namespace DFS.Models
{
    public class File
    {
        public Guid ID { get; set; }
        public Link top_link { get; set; }
        public string file_name {  get; set; }   
        public string file_content { get; set; }
        public Link bottom_link { get; set;}

    }
}
