namespace fordina.Hospital
{
    public class hospital
    {
        public string Title;
        public string[] Diseases;
        public int Capacity;
        public int Fullness;
        public hospital(string title, string[] diseases, int capacity, int fullness)
        {
            Title = title;
            Diseases = diseases;
            Capacity = capacity;
            Fullness = fullness;
        }

        public bool isFull()
        {
            if (Capacity <= Fullness)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddFullness()
        {
            Fullness += 1;
        }
    }
}
