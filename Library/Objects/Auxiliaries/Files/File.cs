using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSI.Library.Objects.Auxiliaries.Files
{
    public class File
    {
        #region Private Fields

        private Int64 _IdFile;
        private String _Name;
        private String _Type;
        private Byte[] _Stream;
        
        #endregion

        internal File(Int64 idFile, String name, String type)
        {
            _IdFile = idFile;
            _Name = name;
            _Type = type;
        }

        #region Public Properties

        public Int64 IdFile
        {
            get { return _IdFile; }
        }
        public String Name
        {
            get { return _Name; }
        }
        public String Type
        {
            get { return _Type; }
        }
        
        public Byte[] Stream
        {
            get
            {
                if (_Stream == null)
                { _Stream = Handlers.Files.StreamByte(_IdFile); }
                return _Stream;
            }
        }

        public static Byte[] FileStream(Int64 idFileResource)
        {
            return Handlers.Files.StreamByte(idFileResource);
        }
        public static MemoryStream MemoryStream(Int64 idFileResource)
        {
            return Handlers.Files.StreamMemory(idFileResource);
        }

        #endregion
    }
}
