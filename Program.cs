// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Threading;

void GenURLTask(int urlcount, string[] domains, int minlength, int length, string wchars)
{
    List<string> strings = new List<string>();
    strings.Add("");
    Random r = new Random();
    int goodurls = 0;

    while (goodurls < urlcount)
    {
        string sequence = "";
        string domain = domains[r.Next(0, domains.Length)];
        while (strings.Contains(sequence + "." + domain) || sequence.Length < minlength)
        {
            domain = domains[r.Next(0, domains.Length)];
            sequence = "";
            for (int i = 0; i < r.Next(minlength, length + 1); i++)
            {
                char c = wchars.ToCharArray()[r.Next(0, wchars.Length)];
                sequence += c.ToString();
            }
        }
        strings.Add(sequence + "." + domain);
        Console.Title = "Checking " + sequence + "." + domain + " (" + Thread.CurrentThread.Name + ")";
        if (RemoteFileExists("http://" + sequence + "." + domain + "/"))
        {
            Console.WriteLine(string.Format("http://{0}.{1}/ found by " + Thread.CurrentThread.Name + "!", sequence, domain));
            goodurls++;
        }
    }
}

bool RemoteFileExists(string url)
{
    try
    {
        HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
        request.Timeout = 5000; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
        request.Method = "HEAD"; //Get only the header information -- no need to download any content

        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        {
            int statusCode = (int)response.StatusCode;
            if (statusCode >= 100 && statusCode < 400) //Good requests
            {
                return true;
            }
            else if (statusCode >= 500 && statusCode <= 510) //Server Errors
            {
                //log.Warn(String.Format("The remote server has thrown an internal error. Url is not valid: {0}", url));
                return false;
            }
        }
    }
    catch (WebException ex)
    {
        if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
        {
            return false;
        }
        else
        {
        }
    }
    catch (Exception ex)
    {
    }
    return false;
}
int length = 50;
int minlength = 6;
int threadcount = 1;
Console.Write("Minimal length? [default: 6] ");
try
{
    minlength = int.Parse(Console.ReadLine());
}
catch
{
}
Console.Write("Maximum length? [default: 50] ");
try
{
    length = int.Parse(Console.ReadLine());
}
catch
{
}


Console.Write("Enter valid set of characters [default: abcdefghijklmnopqrstuvwxyz]: ");
string wchars = Console.ReadLine();
while (wchars == null)
{
    wchars = Console.ReadLine();
}

Console.WriteLine("Select mode of operation:");
Console.WriteLine("\t1. Common TLDs (.com, .net, .org)");
Console.WriteLine("\t2. All TLDs");
Console.WriteLine("\t3. All TLDs, except .ws");
Console.WriteLine("\t4. ____.000webhostapp.com");
Console.WriteLine("\t5. ____.blogspot.com");
string[] domains = new string[] { };
while (true)
{
    bool exitloop = false;
    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.D1:
            domains = new string[] { "com", "net", "org" };
            exitloop = true;
            break;
        case ConsoleKey.D2:
            domains = new string[] { "ac", "ac.uk", "ad", "ae", "aero", "af", "ag", "ai", "al", "am", "an", "ao", "aq", "ar", "arpa", "as", "asia", "at", "au", "aw", "ax", "az", "ba", "bb", "bd", "be", "bf", "bg", "bh", "bi", "biz", "bj", "bm", "bn", "bo", "br", "bs", "bt", "bv", "bw", "by", "bz", "ca", "cat", "cc", "cd", "cf", "cg", "ch", "ci", "ck", "cl", "cm", "cn", "co", "uk", "com", "coop", "cr", "cs", "cu", "cv", "cw", "cx", "cy", "cz", "dd", "de", "dj", "dk", "dm", "do", "dz", "ec", "edu", "ee", "eg", "eh", "er", "es", "et", "eu", "fi", "firm", "fj", "fk", "fm", "fo", "fr", "fx", "ga", "gb", "gd", "ge", "gf", "gg", "gh", "gi", "gl", "gm", "gn", "gov", "uk", "gp", "gq", "gr", "gs", "gt", "gu", "gw", "gy", "hk", "hm", "hn", "hr", "ht", "hu", "id", "ie", "il", "im", "in", "info", "int", "io", "iq", "ir", "is", "it", "je", "jm", "jo", "jobs", "jp", "ke", "kg", "kh", "ki", "km", "kn", "kp", "kr", "kw", "ky", "kz", "la", "lb", "lc", "li", "lk", "lr", "ls", "lt", "uk", "lu", "lv", "ly", "ma", "mc", "md", "me", "uk", "mg", "mh", "mil", "mk", "ml", "mm", "mn", "mo", "mobi", "uk", "mp", "mq", "mr", "ms", "mt", "mu", "museum", "mv", "mw", "mx", "my", "mz", "na", "name", "nato", "nc", "ne", "net", "uk", "nf", "ng", "uk", "ni", "nl", "no", "nom", "np", "nr", "nt", "nu", "nz", "om", "org", "uk", "pa", "pe", "pf", "pg", "ph", "pk", "pl", "uk", "pm", "pn", "posts", "pr", "pro", "ps", "pt", "pw", "py", "qa", "re", "ro", "rs", "ru", "rw", "sa", "sb", "sc", "uk", "sd", "se", "sg", "sh", "si", "sj", "sk", "sl", "sm", "sn", "so", "sr", "ss", "st", "store", "su", "sv", "sy", "sz", "tc", "td", "tel", "tf", "tg", "th", "tj", "tk", "tl", "tm", "tn", "to", "tp", "tr", "travel", "tt", "tv", "tw", "tz", "ua", "ug", "uk", "um", "us", "uy", "uz", "va", "vc", "ve", "vg", "vi", "vn", "vu", "web", "wf", "ws", "xxx", "ye", "yt", "yu", "za", "zm", "zr", "zw" };
            exitloop = true;
            break;
        case ConsoleKey.D3:
            domains = new string[] { "ac", "ac.uk", "ad", "ae", "aero", "af", "ag", "ai", "al", "am", "an", "ao", "aq", "ar", "arpa", "as", "asia", "at", "au", "aw", "ax", "az", "ba", "bb", "bd", "be", "bf", "bg", "bh", "bi", "biz", "bj", "bm", "bn", "bo", "br", "bs", "bt", "bv", "bw", "by", "bz", "ca", "cat", "cc", "cd", "cf", "cg", "ch", "ci", "ck", "cl", "cm", "cn", "co", "uk", "com", "coop", "cr", "cs", "cu", "cv", "cw", "cx", "cy", "cz", "dd", "de", "dj", "dk", "dm", "do", "dz", "ec", "edu", "ee", "eg", "eh", "er", "es", "et", "eu", "fi", "firm", "fj", "fk", "fm", "fo", "fr", "fx", "ga", "gb", "gd", "ge", "gf", "gg", "gh", "gi", "gl", "gm", "gn", "gov", "uk", "gp", "gq", "gr", "gs", "gt", "gu", "gw", "gy", "hk", "hm", "hn", "hr", "ht", "hu", "id", "ie", "il", "im", "in", "info", "int", "io", "iq", "ir", "is", "it", "je", "jm", "jo", "jobs", "jp", "ke", "kg", "kh", "ki", "km", "kn", "kp", "kr", "kw", "ky", "kz", "la", "lb", "lc", "li", "lk", "lr", "ls", "lt", "uk", "lu", "lv", "ly", "ma", "mc", "md", "me", "uk", "mg", "mh", "mil", "mk", "ml", "mm", "mn", "mo", "mobi", "uk", "mp", "mq", "mr", "ms", "mt", "mu", "museum", "mv", "mw", "mx", "my", "mz", "na", "name", "nato", "nc", "ne", "net", "uk", "nf", "ng", "uk", "ni", "nl", "no", "nom", "np", "nr", "nt", "nu", "nz", "om", "org", "uk", "pa", "pe", "pf", "pg", "ph", "pk", "pl", "uk", "pm", "pn", "posts", "pr", "pro", "ps", "pt", "pw", "py", "qa", "re", "ro", "rs", "ru", "rw", "sa", "sb", "sc", "uk", "sd", "se", "sg", "sh", "si", "sj", "sk", "sl", "sm", "sn", "so", "sr", "ss", "st", "store", "su", "sv", "sy", "sz", "tc", "td", "tel", "tf", "tg", "th", "tj", "tk", "tl", "tm", "tn", "to", "tp", "tr", "travel", "tt", "tv", "tw", "tz", "ua", "ug", "uk", "um", "us", "uy", "uz", "va", "vc", "ve", "vg", "vi", "vn", "vu", "web", "wf", "xxx", "ye", "yt", "yu", "za", "zm", "zr", "zw" };
            exitloop = true;
            break;
        case ConsoleKey.D4:
            domains = new string[] { "000webhostapp.com" };
            exitloop = true;
            break;
        case ConsoleKey.D5:
            domains = new string[] { "blogspot.com" };
            exitloop = true;
            break;
    }
    if (exitloop)
    {
        break;
    }
}
Console.WriteLine();
Console.Write("Number of threads to dedicate? [default: 1] ");
try
{
    threadcount = int.Parse(Console.ReadLine());
} catch
{
}
Console.WriteLine();
if (wchars == "") { wchars = "abcdefghijklmnopqrstuvwxyz"; }

Console.Write("Number of URLs to generate: ");
int urlcount = 500;
try
{
    urlcount = int.Parse(Console.ReadLine());
} catch
{

}
Console.WriteLine();
Thread[] threads = new Thread[threadcount];
for (int i = 0; i < threadcount; i++)
{
    threads[i] = new Thread(() => GenURLTask(urlcount / threadcount, domains, minlength, length, wchars))
    {
        Name = "Thread" + i.ToString()
    };
    threads[i].Start();
}
foreach (Thread thread in threads)
{
    thread.Join();
}
Console.Write("Press any key to exit . . . ");
Console.ReadKey();