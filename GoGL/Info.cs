using GoGL.Graphics;
using IL2CPU.API.Attribs;

namespace GoGL;

public class Info
{
    [ManifestResourceStream(ResourceName = "GoGL.Resources.logo.bmp")]
    static byte[] GoGLLogoRaw;

    public Canvas GoGLLogo;

    public String getVersion()
    {
        return "0.0.4";
    }

    public String getApiVersion()
    {
        return "1.3";
    }

    public Canvas getLogo()
    {
        GoGLLogo = Image.FromBitmap(GoGLLogoRaw, false);
        return GoGLLogo;
    }
}