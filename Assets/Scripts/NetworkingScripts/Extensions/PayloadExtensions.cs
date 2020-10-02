using System.Text;

namespace NetworkingScripts.Extensions {
  public static class PayloadExtensions {
    public static byte[] GetBytes(this string str) {
      return Encoding.UTF8.GetBytes(str);
    }

    public static string GetString(this byte[] bytes) {
      return Encoding.UTF8.GetString(bytes);
    }
  }
}
