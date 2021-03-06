using System;
using System.Runtime.InteropServices;

/*
 * Imported from https://github.com/gsuberland/MultiProcessorExtensions
 * Copyright (c) 2019 Graham Sutherland
 */
namespace LowLevelDesign.Win32.NUMA
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GROUP_AFFINITY
    {
        public UIntPtr Mask;
        public UInt16 Group;
        private UInt16 Reserved0;
        private UInt16 Reserved1;
        private UInt16 Reserved2;
    }

    internal static class NativeMethods
    {
        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetNumaHighestNodeNumber(out ulong highestNodeNumber);
        
        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetNumaNodeProcessorMaskEx(
            ushort node,
            ref GROUP_AFFINITY processorMask
        );

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetThreadGroupAffinity(
            IntPtr handle,
            ref GROUP_AFFINITY affinity
        );

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetThreadGroupAffinity(
            IntPtr handle,
            ref GROUP_AFFINITY affinity,
            ref GROUP_AFFINITY previousAffinity
        );

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetProcessGroupAffinity(
            IntPtr handle,
            ref ushort GroupCount,
            [In, Out] ushort[] GroupArray
        );
    }
}