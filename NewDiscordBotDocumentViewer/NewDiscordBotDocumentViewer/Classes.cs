using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDiscordBotDocumentViewer
{
    [Serializable]
    public class EmbedDocument
    {
        public List<string> Titles { get; set; } = new List<string>();
        public List<string> Contents { get; set; } = new List<string>();
        public string Bookmark { get; set; } = "";

        public Dictionary<string, string> ConvertToDictionary()
        {
            Dictionary<string, string> Return = new Dictionary<string, string>();
            int count = 0;
            foreach (var a in Titles)
            {
                Return.Add(a, Contents[count]);
                count++;
            }
            return Return;
        }

        public void ConvertToInternalData(Dictionary<string, string> Data)
        {
            Titles = new List<string>();
            Contents = new List<string>();

            foreach (var a in Data)
            {
                Titles.Add(a.Key);
                Contents.Add(a.Value);
            }
        }

        public void AddDataPair(string Title, string Content)
        {
            Titles.Add(Title);
            Contents.Add(Content);
        }
    }

    [Serializable]
    public class XMLTypeEmbedDocuments
    {
        public List<EmbedDocument> Pages { get; set; } = new List<EmbedDocument>();
        public string Title { get; set; } = "";
    }
}
