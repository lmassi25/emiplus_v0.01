namespace Emiplus.Model
{
    using Data.Database;
    using SqlKata;
    using SqlKata.Execution;

    class Config : Model
    {
        public Config() : base("CONFIG") {}

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public string Config_Key { get; set; }
        public string Config_Value { get; set; }


        public Config ChangeKey(string key)
        {
            return FindAll().Where("config_key", key).First<Config>();
        }

        public Config SetValue(string value)
        {
            Config_Value = value;
            return this;
        }

        public void Save()
        {
            Data(this).Update("ID", Id);
        }
    }
}
