// <copyright file="NativeTypes.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Runtime.InteropServices;

namespace Managed.LLama.Cpp.Native;

[StructLayout(LayoutKind.Sequential)]
public struct llama_model{}

[StructLayout(LayoutKind.Sequential)]
public struct llama_context{}

[StructLayout(LayoutKind.Sequential)]
public struct ggml_context{}

[StructLayout(LayoutKind.Sequential)]
public struct ggml_backend{}

[StructLayout(LayoutKind.Sequential)]
public struct ggml_opt_dataset{}

[StructLayout(LayoutKind.Sequential)]
public struct ggml_backend_sched{}

[StructLayout(LayoutKind.Sequential)]
public struct ggml_opt_context{}

[StructLayout(LayoutKind.Sequential)]
public struct ggml_backend_device{}

[StructLayout(LayoutKind.Sequential)]
public struct ggml_backend_reg{}

[StructLayout(LayoutKind.Sequential)]
public struct ggml_backend_buffer{}

[StructLayout(LayoutKind.Sequential)]
public struct ggml_backend_buffer_type{}

[StructLayout(LayoutKind.Sequential)]
public struct ggml_threadpool{}

[StructLayout(LayoutKind.Sequential)]
public struct gguf_context{}

[StructLayout(LayoutKind.Sequential)]
public struct llama_vocab{}

[StructLayout(LayoutKind.Sequential)]
public struct ggml_cgraph { }

[StructLayout(LayoutKind.Sequential)]
public struct ggml_object { }

[StructLayout(LayoutKind.Sequential)]
public struct ggml_backend_event { }

[StructLayout(LayoutKind.Sequential)]
public struct ggml_opt_result { }

[StructLayout(LayoutKind.Sequential)]
public struct llama_memory_i { }

[StructLayout(LayoutKind.Sequential)]
public struct llama_adapter_lora { }

[StructLayout(LayoutKind.Sequential)]
public struct _iobuf { }

[StructLayout(LayoutKind.Sequential)]
public struct mtmd_context { }

[StructLayout(LayoutKind.Sequential)]
public struct mtmd_bitmap { }

[StructLayout(LayoutKind.Sequential)]
public struct mtmd_input_chunks { }

[StructLayout(LayoutKind.Sequential)]
public struct mtmd_input_chunk { }

[StructLayout(LayoutKind.Sequential)]
public struct mtmd_image_tokens { }

[StructLayout(LayoutKind.Sequential)]
public struct mtmd_helper_video { }
