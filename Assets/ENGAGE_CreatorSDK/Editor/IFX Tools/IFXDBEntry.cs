using System.Collections.Generic;
namespace IFXTools {
    public class IFXDBEntry
    {
        //Name
        public string itemName { get; set; }
        //PrettyNName
        public string itemPrettyName { get; set; }

        //Catagory
        public string category{ get; set; }
        
        //Bundle
        public string ItemBundleLabel { get; set; }

        //Premuim
        public bool itemPremium { get; set; }

        //Editor Only
        public bool itemEditorOnly { get; set; }

        //Admin Only
        public bool itemAdminOnly{ get; set; } = true;

        //Unlisted
        public bool itemUnlisted { get; set; } =true;

        //Private
        public bool itemPrivate { get; set; } =true;

        //Tags
        public string tags { get; set; }

        //Type
        public string type{ get; set; }

        //Collections
        public string collections { get; set; }

        //PC Only
        public bool itemPCOnly { get; set; } = false;

        //Status
        public string status { get; set; }
        
        //IFX Options
        public string options { get; set; }

        //created at time
        public string createdAtTime { get; set; }
    }

    public class IFXDBTags
    {
        string _tagType;
        public string[] options { get; set; } // maybe pull tags list from DB? or store in easier to edit json file
        public int index { get; set; }
        public List<string> usedTags { get; set; } = new List<string>();
        public string result { get; set; }
        
        
        public IFXDBTags(string tagType)
        {
            _tagType = tagType;
            options = GetTagOptionsFromDB();
            index=0;
        }
       public string GetTagType()
        {
            return _tagType;
        }
        
         string[] GetTagOptionsFromDB()
        {
            string[] output;
            //This needs to hook up to the DB this is temporarry for testing
            output = new string[] {"-Select Option here-", "--Clear Options--", "Animals", "Sphere", "Plane"};
            
            return output;

        }
    }

    public class IFXDBDropDownList
    {
        string _optionListType;
        public string[] options { get; set; } // maybe pull tags list from DB? or store in easier to edit json file
        public int index { get; set; }
        public string result { get; set; }
        
        
        public IFXDBDropDownList(string tagType)
        {
            _optionListType = tagType;
            options = GetTagOptionsFromDB();
            index=0;
        }
       public string GetDropDownListType()
        {
            return _optionListType;
        }
        
         string[] GetTagOptionsFromDB()
        {
            string[] output;
            //This needs to hook up to the DB this is temporarry for testing
            output = new string[] {"-Select Option here-", "--Clear Options--", "Animals", "Sphere", "Plane"};
            
            return output;

        }
    }
}