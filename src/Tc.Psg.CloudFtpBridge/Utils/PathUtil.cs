using System.Linq;
using System.Text;

namespace Tc.Psg.CloudFtpBridge.Utils
{
    public static class PathUtil
    {
        public static string CombineFragments(params string[] fragments)
        {
            StringBuilder builder = new StringBuilder();

            foreach (string fragment in fragments.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                // always use forward slashes
                string normalizedFragment = fragment.Replace('\\', '/');
                
                // always lead with "/" and never trail with "/"
                if (!normalizedFragment.StartsWith("/"))
                {
                    normalizedFragment = string.Concat("/", normalizedFragment);
                }

                if (normalizedFragment.EndsWith("/"))
                {
                    normalizedFragment = normalizedFragment.Substring(0, normalizedFragment.Length - 1);
                }

                // discard lonely "/"
                if (normalizedFragment.Equals("/"))
                {
                    continue;
                }

                builder.Append(normalizedFragment);
            }

            return builder.ToString();
        }

        public static string GetFileName(string fullPath)
        {
            fullPath = fullPath ?? string.Empty;

            if (fullPath.Contains("/"))
            {
                return fullPath.Substring(fullPath.LastIndexOf("/") + 1);
            }

            else
            {
                return fullPath;
            }
        }
    }
}
