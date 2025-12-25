namespace GAS.Common
{
    public class GASResponseChecker
    {
        public static void EnsureSuccess<T>(GASCommonResp<T> resp)
        {
            if (resp == null) throw new GASParseException("Response is null");
            if (resp.Code != 200) throw new GASException(resp.Code, resp.Msg ?? "Unknown error");
        }
    }
}