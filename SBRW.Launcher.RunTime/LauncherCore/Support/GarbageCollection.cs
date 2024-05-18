using System;
/// <summary>
/// 
/// </summary>
public static class GarbageCollections
{
    /// <summary>
    /// Force two garbage collections to release memory that is no
    /// longer referenced but has not been released yet
    /// </summary>
    public static void Cleanup()
    {
        //GC.Collect(2, GCCollectionMode.Forced);
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        GC.WaitForPendingFinalizers();
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        //GC.Collect();
    }
}