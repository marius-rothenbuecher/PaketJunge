﻿namespace PaketJunge.Packets.PROFINET
{
    public enum DCPType
    {
        Hello = 0xfefc,
        GetOrSet = 0xfefd,
        IdentRequest = 0xfefe,
        IdentReset = 0xfeff
    }
}
