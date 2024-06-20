using System;
using System.Runtime.InteropServices;

namespace SBRW.Concepts
{
    public static class CoreCount
    {
        /// <summary>
        /// Retrieves information about logical processors and related hardware.
        /// </summary>
        /// <param name="buffer">
        /// A pointer to a buffer that receives an array of <see cref="SYSTEM_LOGICAL_PROCESSOR_INFORMATION"/> structures. 
        /// If the function fails, the contents of this buffer are undefined.
        /// </param>
        /// <param name="bufferSize">
        /// On input, specifies the length of the buffer pointed to by Buffer, in bytes. 
        /// If the buffer is large enough to contain all of the data, this function succeeds and ReturnLength is set to the number of bytes returned.
        /// </param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetLogicalProcessorInformation(IntPtr buffer, ref int bufferSize);
        /// <summary>
        /// Describes the cache attributes.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CACHE_DESCRIPTOR
        {
            /// <summary>
            /// The cache level. This member can currently be one of the following values; 
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-cache_descriptor#members">other values</see> 
            /// may be supported in the future.
            /// </summary>
            public byte Level;
            /// <summary>
            /// The cache associativity. If this member is CACHE_FULLY_ASSOCIATIVE (0xFF), the cache is fully associative.
            /// </summary>
            public byte Associativity;
            /// <summary>
            /// The cache line size, in bytes.
            /// </summary>
            public ushort LineSize;
            /// <summary>
            /// The cache size, in bytes.
            /// </summary>
            public uint Size;
            /// <summary>
            /// The cache type. This member is a PROCESSOR_CACHE_TYPE value.
            /// </summary>
            public uint Type;
        }
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION_UNION
        {
            /// <summary>
            /// 
            /// </summary>
            [FieldOffset(0)] public byte ProcessorCore;
            /// <summary>
            /// 
            /// </summary>
            [FieldOffset(0)] public uint NumaNode;
            /// <summary>
            /// 
            /// </summary>
            [FieldOffset(0)] public CACHE_DESCRIPTOR Cache;
            /// <summary>
            /// 
            /// </summary>
            [FieldOffset(0)] private ulong Reserved1;
            /// <summary>
            /// 
            /// </summary>
            [FieldOffset(8)] private ulong Reserved2;
        }
        /// <summary>
        /// Represents the relationship between the processor set identified in the corresponding 
        /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-system_logical_processor_information_ex">
        /// SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX</see> structure.
        /// </summary>
        public enum LOGICAL_PROCESSOR_RELATIONSHIP
        {
            /// <summary>
            /// The specified logical processors share a single processor core.
            /// </summary>
            RelationProcessorCore,
            /// <summary>
            /// The specified logical processors are part of the same NUMA node.
            /// </summary>
            RelationNumaNode,
            /// <summary>
            /// The specified logical processors share a cache. 
            /// </summary>
            /// <remarks>Windows Server 2003: 
            /// This value is not supported until Windows Server 2003 with SP1 and Windows XP Professional x64 Edition.
            /// </remarks>
            RelationCache,
            /// <summary>
            /// The specified logical processors share a physical package (a single package socketed or soldered onto a motherboard may contain multiple processor cores or threads, each of which is treated as a separate processor by the operating system).
            /// </summary>
            /// <remarks>Windows Server 2003:  
            /// This value is not supported until Windows Server 2003 with SP1 and Windows XP Professional x64 Edition.
            /// </remarks>
            RelationProcessorPackage,
            /// <summary>
            /// The specified logical processors share a single 
            /// <see href="https://learn.microsoft.com/en-us/windows/desktop/ProcThread/processor-groups">processor group</see>.
            /// </summary>
            /// <remarks>Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP Professional x64 Edition:  
            /// This value is not supported until Windows Server 2008 R2.
            /// </remarks>
            RelationGroup,
            /// <summary>
            /// On input, retrieves information about all possible relationship types. This value is not used on output.
            /// </summary>
            /// <remarks>Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP Professional x64 Edition:  
            /// This value is not supported until Windows Server 2008 R2.</remarks>
            RelationAll = 0xffff
        }
        /// <summary>
        /// Describes the relationship between the specified processor set. 
        /// This structure is used with the <see cref="GetLogicalProcessorInformation"/> function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION
        {
            /// <summary>
            /// The processor mask identifying the processors described by this structure. 
            /// A processor mask is a bit vector in which each set bit represents an active processor in the relationship. 
            /// At least one bit will be set.
            /// On a system with more than 64 processors, the processor mask identifies processors in a single 
            /// <see href="https://learn.microsoft.com/en-us/windows/desktop/ProcThread/processor-groups">processor group</see>.
            /// </summary>
            public UIntPtr ProcessorMask;
            /// <summary>
            /// The relationship between the processors identified by the value of the ProcessorMask member. 
            /// This member can be one of the following <see cref="LOGICAL_PROCESSOR_RELATIONSHIP"/> values.
            /// </summary>
            public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;
            /// <summary>
            /// 
            /// </summary>
            public SYSTEM_LOGICAL_PROCESSOR_INFORMATION_UNION ProcessorInformation;
        }
        /// <summary>
        /// The number of physical processors on the current machine.
        /// </summary>
        /// <returns>The number of physical processors that are available for use</returns>
        public static int GetProcessorCoreCount()
        {
            return GetProcessorCoreCount(false);
        }
        /// <summary>
        /// The number of physical processors on the current machine.
        /// </summary>
        /// <returns>The number of physical processors that are available for use</returns>
        public static int GetProcessorCoreCount(bool Logical)
        {
            int bufferSize = 0;
            if (!GetLogicalProcessorInformation(IntPtr.Zero, ref bufferSize) && Marshal.GetLastWin32Error() != 122)
            {
                return Environment.ProcessorCount;
            }
            else
            {
                IntPtr buffer = Marshal.AllocHGlobal(bufferSize);

                try
                {
                    if (!GetLogicalProcessorInformation(buffer, ref bufferSize))
                    {
                        return Environment.ProcessorCount;
                    }
                    else
                    {
                        int numEntries = bufferSize / Marshal.SizeOf<SYSTEM_LOGICAL_PROCESSOR_INFORMATION>();
                        int cores = 0;

                        for (int i = 0; i < numEntries; i++)
                        {
                            IntPtr itemPtr = new IntPtr(buffer.ToInt64() + i * Marshal.SizeOf<SYSTEM_LOGICAL_PROCESSOR_INFORMATION>());
                            SYSTEM_LOGICAL_PROCESSOR_INFORMATION info = Marshal.PtrToStructure<SYSTEM_LOGICAL_PROCESSOR_INFORMATION>(itemPtr);

                            if (Logical)
                            {
                                /* Count all logical processors */
                                cores += CountBits(info.ProcessorMask.ToUInt64());
                            }
                            else
                            {
                                if (info.Relationship == LOGICAL_PROCESSOR_RELATIONSHIP.RelationProcessorCore)
                                {
                                    cores++;
                                }
                            }
                        }

                        return cores > 0 ? cores : 1;
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        private static int CountBits(ulong mask)
        {
            int count = 0;
            while (mask != 0)
            {
                mask &= (mask - 1); // Clear the lowest set bit
                count++;
            }
            return count;
        }
        /// <summary>
        /// The number of physical processors on the current machine.
        /// </summary>
        /// <returns>The number of physical processors that are available for use</returns>
        /// <remarks>If function fails it will return the amount of logical processors</remarks>
        public static readonly int ProcessorPhysicalCores = GetProcessorCoreCount();
        /// <summary>
        /// The number of logical processors on the current machine.
        /// </summary>
        /// <returns>The number of logical processors that are available for use</returns>
        public static readonly int ProcessorLogicalCores = GetProcessorCoreCount(true);
    }
}
