
namespace QHStudio.Game
{
    public struct ValueLine
    {
        float start;
        float end;

        public float Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }

        public float End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
            }
        }

        public ValueLine(float start, float end)
        {
            this.start = start;
            this.end = end;
        }

        public bool inLine(float v)
        {
            return v >= this.start && v < end;
        }
    }
}