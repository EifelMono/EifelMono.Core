namespace EifelMono.Core.System
{
    public class Arguments
    {
        private string _Line;
        public string Line
        {
            get => _Line;
            set
            {
                _Line = value;
                if (_Line != null)
                    LineArray = _Line.Split(' ');
                else
                    LineArray = null;
            }
        }
        public string[] LineArray;
        public Arguments(string line = null)
        {
            Line = line;
        }
    }
}
