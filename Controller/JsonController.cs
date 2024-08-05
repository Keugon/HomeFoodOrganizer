using System.Text.Json;

namespace Essensausgleich.Controller
{
    /// <summary>
    /// Generic JsonController for Save and Load of Obejcts
    /// </summary>
    public abstract class JsonController<T> : DRAXNET.Core.AppObjekt
    {

        /// <summary>
        /// Generic method to read a Json File
        /// </summary>
        /// <param name="FilePathAndName">File path and Name</param>
        /// <returns></returns>
        public T Load(string FilePathAndName)
        {
            var FileStream = File.ReadAllText(FilePathAndName);
            var option = new JsonSerializerOptions
            {
                //ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true,
                MaxDepth = 10,
                IncludeFields = true
            };

            var jsonDeserializer = JsonSerializer.Deserialize(FileStream, typeof(T), option);
            return (T)jsonDeserializer!;

        }
        /// <summary>
        /// Generic mehtod for Saving to Json from a generic object
        /// </summary>
        /// <param name="FilePathAndName">File path and Name</param>
        /// <param name="data">contents of object</param>
        public void Save(string FilePathAndName, T data)
        {

            var option = new JsonSerializerOptions
            {
                //ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true,
                MaxDepth = 10,
                IncludeFields = true
            };


            var jsonSerializer = JsonSerializer.Serialize(data, option);
            File.WriteAllText(FilePathAndName, jsonSerializer);


        }
    }
}
