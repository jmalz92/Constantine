using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Constantine
{
    /// <summary>
    /// Class to load/serialize/update the game's save data
    /// </summary>
    [Serializable, XmlRoot("SaveData")]
    public class SaveData
    {

        public int EasyScore { get; set; }
        public int NormalScore { get; set; }
        public int HardScore { get; set; }
        public bool CinematicViewed { get; set; }

        private SaveData()
        {
            EasyScore = 0;
            NormalScore = 0;
            HardScore = 0;
            CinematicViewed = false;
        }

        /// <summary>
        /// Loads the save file
        /// </summary>
        /// <param name="fileName">the name of the save file</param>
        /// <returns></returns>
        public static SaveData Load(string fileName)
        {
            SaveData data = null;

            if (!File.Exists(fileName))
                return new SaveData();
            
            FileStream stream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                data = (SaveData)serializer.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }

            return data;
        }

        /// <summary>
        /// Saves the data back to a save file
        /// </summary>
        /// <param name="data">the data to save</param>
        /// <param name="fileName">the name of the file</param>
        public static void Save(SaveData data, string fileName)
        {
            FileStream stream = File.Open(fileName, FileMode.OpenOrCreate);
            try
            {
                // Convert the object to XML data and put it in the stream
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                serializer.Serialize(stream, data);
            }
            catch (Exception ex) 
            { 
            }
            finally
            {
                // Close the file
                stream.Close();
            }
        }
    }
}
