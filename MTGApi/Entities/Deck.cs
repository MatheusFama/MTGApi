using MTGApi.Helpers;

namespace MTGApi.Entities
{
    public class Deck
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }

        private string _deckCode;
        public string DeckCode 
        { 
            get
            {
                return string.IsNullOrEmpty(_deckCode)?"" : StringCompresser.Decompress(_deckCode);
            }
            set
            {
                _deckCode = StringCompresser.Compress(value);
            }
        }

        public virtual Account Account { get; set; }

    }
}
