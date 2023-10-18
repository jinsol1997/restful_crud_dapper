using System.Transactions;
using restful_crud_dapper.Models;

namespace restful_crud_dapper.Services{
    public class LanguageService{
        private readonly LanguageRepository _languageRepository;

        public LanguageService(LanguageRepository languageRepository){
            _languageRepository = languageRepository;
        }

        public async Task<List<Language>> SelectAll(){
            return await _languageRepository.SelectAll();
        }

        public async Task<Language> SelectById(int id){
            return await _languageRepository.SelectById(id);
        }

        public async Task<int> CreateLanguage(Language language){

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)){
                
                try {
                    int result = await _languageRepository.Insert(language);
                    transaction.Complete();
                    return result;
                    }
                catch(Exception e) {
                    transaction.Dispose();
                    throw;
                }
                
            }

        }

        public async Task<int> UpdateLanguage(Language language){
            return await _languageRepository.Update(language);
        }

        public async Task<int> DeleteLanguage(int id){
            return await _languageRepository.DeleteById(id);
        }

    }
}