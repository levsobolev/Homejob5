using System.Collections.Generic;
namespace fordina.Grandmother
{
    public struct grandmother
    {
        public string Name;
        public byte Age;
        public Dictionary<string, string> Diseases;

        public grandmother(string name, byte age)
        {
            Name = name;
            Age = age;
            Diseases = new Dictionary<string, string>();
        }
        public grandmother(string name, byte age,  Dictionary<string, string> diseases)
        {
            Name = name;
            Age = age;
            Diseases = diseases;
        }
    }
}
