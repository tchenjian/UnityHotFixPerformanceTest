/*
 * Tencent is pleased to support the open source community by making InjectFix available.
 * Copyright (C) 2019 THL A29 Limited, a Tencent company.  All rights reserved.
 * InjectFix is licensed under the MIT License, except for the third-party components listed in the file 'LICENSE' which may be subject to their corresponding license terms. 
 * This file is subject to the terms and conditions defined in file 'LICENSE', which is part of this source code package.
 */

namespace IFix.Core
{
    public enum Code
    {
        Bgt,
        Bge,
        Ldobj,
        Conv_Ovf_I8,
        Refanytype,
        Newanon,
        Ldind_I8,
        Conv_Ovf_I1_Un,
        Conv_Ovf_U1_Un,
        Mul_Ovf_Un,
        Conv_I1,
        Endfinally,
        Clt,
        Callvirtvirt,
        Add_Ovf,
        Sub_Ovf,
        Conv_U8,
        Conv_U4,
        Switch,
        Conv_R8,
        Ldind_Ref,
        Ldvirtftn2,
        Stelem_Ref,
        Cgt_Un,
        Stelem_R8,
        Rem,
        Volatile,
        Ldind_U1,
        Stelem_R4,
        Newarr,
        Stind_I8,
        Ldelem_U4,
        Stsfld,
        Call,
        Stloc,
        Leave,
        No,
        Conv_Ovf_I2,
        Div_Un,
        Starg,
        Stelem_I4,
        Conv_Ovf_U2_Un,
        Conv_Ovf_I2_Un,
        Blt_Un,
        Brtrue,
        Ldfld,
        Localloc,
        Ldtype, // custom
        And,
        Stind_R8,
        Shr_Un,
        Sub,
        Brfalse,
        Conv_I,
        Ldlen,
        Rem_Un,
        Sub_Ovf_Un,
        Conv_Ovf_I1,
        Refanyval,
        Conv_U2,
        Conv_Ovf_U4_Un,
        Nop,
        Ldind_R4,
        Conv_Ovf_I,
        Mul,
        Add,
        Ldind_U4,

        //Pseudo instruction
        StackSpace,
        Conv_Ovf_I_Un,
        Castclass,
        Rethrow,
        Ldvirtftn,
        Conv_Ovf_U_Un,
        Isinst,
        Ldelem_U1,
        Conv_Ovf_U8,
        Stelem_I1,
        Bne_Un,
        Unbox,
        Initobj,
        Callvirt,
        Ldind_I,
        Div,
        Ldelem_I,
        Ldelem_I8,
        Ble_Un,
        Ldloc,
        Initblk,
        Clt_Un,
        Cpobj,
        Bgt_Un,
        Stelem_I,
        Stelem_Any,
        Ldstr,
        Ldelema,
        Br,
        Ldc_I8,
        Ldtoken,
        Conv_Ovf_I4,
        Ldsflda,
        Shl,
        Tail,
        Mkrefany,
        Ldind_I1,
        Neg,
        Sizeof,
        Conv_R4,
        Stind_I4,
        Or,
        Conv_Ovf_U1,
        //Calli,
        Not,
        Readonly,
        Ldelem_U2,
        Mul_Ovf,
        Ldelem_R4,
        Endfilter,
        Stobj,
        Ldloca,
        Ldelem_R8,
        Ldflda,
        Ble,
        Conv_Ovf_U2,
        Bge_Un,
        Conv_U1,
        Arglist,
        Ldarg,
        Ret,
        Box,
        Ldc_R4,
        Conv_Ovf_I4_Un,
        Stind_I,
        Ldelem_Any,
        Shr,
        Ldc_R8,
        Stind_I1,
        Conv_Ovf_U8_Un,
        Stelem_I8,
        Jmp,
        Conv_R_Un,
        Add_Ovf_Un,
        Conv_Ovf_U,
        Conv_I8,
        Stfld,
        CallExtern,
        Cgt,
        Blt,
        Ldelem_I1,
        Break,
        Stind_R4,
        Stind_Ref,
        Unbox_Any,
        Ckfinite,
        Cpblk,
        Xor,
        Ldelem_I2,
        Conv_U,
        Ldind_I4,
        Ldind_U2,
        Ldelem_Ref,
        Constrained,
        Ldarga,
        Ldind_I2,
        Ldc_I4,
        Ceq,
        Stind_I2,
        Dup,
        Ldind_R8,
        Stelem_I2,
        Conv_I4,
        Conv_Ovf_I8_Un,
        Ldsfld,
        Ldelem_I4,
        Unaligned,
        Pop,
        Beq,
        Ldftn,
        Conv_Ovf_U4,
        Throw,
        Conv_I2,
        Ldnull,
        Newobj,
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Instruction
    {
        /// <summary>
        /// 指令MAGIC
        /// </summary>
        public const ulong INSTRUCTION_FORMAT_MAGIC = 3496696313403114631;

        /// <summary>
        /// 当前指令
        /// </summary>
        public Code Code;

        /// <summary>
        /// 操作数
        /// </summary>
        public int Operand;
    }

    public enum ExceptionHandlerType
    {
        Catch = 0,
        Filter = 1,
        Finally = 2,
        Fault = 4
    }

    public sealed class ExceptionHandler
    {
        public System.Type CatchType;
        public int CatchTypeId;
        public int HandlerEnd;
        public int HandlerStart;
        public ExceptionHandlerType HandlerType;
        public int TryEnd;
        public int TryStart;
    }
}