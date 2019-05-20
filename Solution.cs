class Solution
{
    public int Kyu { get; set; }
    public string Name { get; set; }
    public string Language { get; set; }
    public string Code { get; set; }

    public Solution(int kyu, string name, string language, string code)
    {
        Kyu = kyu;
        Name = name;
        Language = language;
        Code = code;
    }
}