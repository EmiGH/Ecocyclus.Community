using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class EmissionFactorLanguageOptions
    {
        internal EmissionFactorLanguageOptions() { }
        
        #region Read Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.EmissionFactorLanguageOption Item(Int64 idEmissionFactor, String idLanguage)
        {
            Storage.EmissionFactorLanguageOptions _dbEmissionFactorLanguageOptions = new Storage.EmissionFactorLanguageOptions();
            Library.Objects.Auxiliaries.EmissionFactors.EmissionFactorLanguageOption _emissionFactorLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactorLanguageOptions.ReadById(idEmissionFactor, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _emissionFactorLanguageOption = new Library.Objects.Auxiliaries.EmissionFactors.EmissionFactorLanguageOption(idLanguage, Convert.ToString(_dbRecord["Description"]));
            }
            return _emissionFactorLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Auxiliaries.EmissionFactors.EmissionFactorLanguageOption> Items(Int64 idEmissionFactor)
        {
            Dictionary<String, Library.Objects.Auxiliaries.EmissionFactors.EmissionFactorLanguageOption> _oItems = new Dictionary<String, Library.Objects.Auxiliaries.EmissionFactors.EmissionFactorLanguageOption>();
            Storage.EmissionFactorLanguageOptions _dbEmissionFactorLanguageOptions = new Storage.EmissionFactorLanguageOptions();
            Library.Objects.Auxiliaries.EmissionFactors.EmissionFactorLanguageOption _emissionFactorLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactorLanguageOptions.ReadAll(idEmissionFactor);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _emissionFactorLanguageOption = new Library.Objects.Auxiliaries.EmissionFactors.EmissionFactorLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Description"]));

                _oItems.Add(_idLanguage, _emissionFactorLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.EmissionFactorLanguageOption Add(Int64 idEmissionFactor, String idLanguage, String description)
        {
            Storage.EmissionFactorLanguageOptions _dbEmissionFactorLanguageOptions = new Storage.EmissionFactorLanguageOptions();

            try
            {
                _dbEmissionFactorLanguageOptions.Create(idEmissionFactor, idLanguage, description);
                return new Objects.Auxiliaries.EmissionFactors.EmissionFactorLanguageOption(idLanguage, description);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idEmissionFactor, String idLanguage)
        {
            Storage.EmissionFactorLanguageOptions _dbEmissionFactorLanguageOptions = new Storage.EmissionFactorLanguageOptions();

            try
            {
                _dbEmissionFactorLanguageOptions.Delete(idEmissionFactor, idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idEmissionFactor, String idLanguage, String description)
        {
            Storage.EmissionFactorLanguageOptions _dbEmissionFactorLanguageOptions = new Storage.EmissionFactorLanguageOptions();

            try
            {
                _dbEmissionFactorLanguageOptions.Update(idEmissionFactor, idLanguage, description);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
            
        }

        #endregion
    }
}
