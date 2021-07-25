using System.Threading.Tasks;

namespace MedianViaTakeprofittech.Application {
    public interface IResponsesProvider {
        public Task<int[]> GetValues();
    }
}
