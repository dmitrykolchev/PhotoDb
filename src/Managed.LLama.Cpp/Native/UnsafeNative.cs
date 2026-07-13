using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Managed.LLama.Cpp.Native
{
    public enum ggml_status
    {
        GGML_STATUS_ALLOC_FAILED = -2,
        GGML_STATUS_FAILED = -1,
        GGML_STATUS_SUCCESS = 0,
        GGML_STATUS_ABORTED = 1,
    }

    public partial struct ggml_bf16_t
    {
        [NativeTypeName("uint16_t")]
        public ushort bits;
    }

    public enum ggml_type
    {
        GGML_TYPE_F32 = 0,
        GGML_TYPE_F16 = 1,
        GGML_TYPE_Q4_0 = 2,
        GGML_TYPE_Q4_1 = 3,
        GGML_TYPE_Q5_0 = 6,
        GGML_TYPE_Q5_1 = 7,
        GGML_TYPE_Q8_0 = 8,
        GGML_TYPE_Q8_1 = 9,
        GGML_TYPE_Q2_K = 10,
        GGML_TYPE_Q3_K = 11,
        GGML_TYPE_Q4_K = 12,
        GGML_TYPE_Q5_K = 13,
        GGML_TYPE_Q6_K = 14,
        GGML_TYPE_Q8_K = 15,
        GGML_TYPE_IQ2_XXS = 16,
        GGML_TYPE_IQ2_XS = 17,
        GGML_TYPE_IQ3_XXS = 18,
        GGML_TYPE_IQ1_S = 19,
        GGML_TYPE_IQ4_NL = 20,
        GGML_TYPE_IQ3_S = 21,
        GGML_TYPE_IQ2_S = 22,
        GGML_TYPE_IQ4_XS = 23,
        GGML_TYPE_I8 = 24,
        GGML_TYPE_I16 = 25,
        GGML_TYPE_I32 = 26,
        GGML_TYPE_I64 = 27,
        GGML_TYPE_F64 = 28,
        GGML_TYPE_IQ1_M = 29,
        GGML_TYPE_BF16 = 30,
        GGML_TYPE_TQ1_0 = 34,
        GGML_TYPE_TQ2_0 = 35,
        GGML_TYPE_MXFP4 = 39,
        GGML_TYPE_NVFP4 = 40,
        GGML_TYPE_Q1_0 = 41,
        GGML_TYPE_COUNT = 42,
    }

    public enum ggml_prec
    {
        GGML_PREC_DEFAULT = 0,
        GGML_PREC_F32 = 10,
    }

    public enum ggml_op_hint
    {
        GGML_HINT_NONE = 0,
        GGML_HINT_SRC0_IS_HADAMARD = 1,
    }

    public enum ggml_ftype
    {
        GGML_FTYPE_UNKNOWN = -1,
        GGML_FTYPE_ALL_F32 = 0,
        GGML_FTYPE_MOSTLY_F16 = 1,
        GGML_FTYPE_MOSTLY_Q4_0 = 2,
        GGML_FTYPE_MOSTLY_Q4_1 = 3,
        GGML_FTYPE_MOSTLY_Q4_1_SOME_F16 = 4,
        GGML_FTYPE_MOSTLY_Q8_0 = 7,
        GGML_FTYPE_MOSTLY_Q5_0 = 8,
        GGML_FTYPE_MOSTLY_Q5_1 = 9,
        GGML_FTYPE_MOSTLY_Q2_K = 10,
        GGML_FTYPE_MOSTLY_Q3_K = 11,
        GGML_FTYPE_MOSTLY_Q4_K = 12,
        GGML_FTYPE_MOSTLY_Q5_K = 13,
        GGML_FTYPE_MOSTLY_Q6_K = 14,
        GGML_FTYPE_MOSTLY_IQ2_XXS = 15,
        GGML_FTYPE_MOSTLY_IQ2_XS = 16,
        GGML_FTYPE_MOSTLY_IQ3_XXS = 17,
        GGML_FTYPE_MOSTLY_IQ1_S = 18,
        GGML_FTYPE_MOSTLY_IQ4_NL = 19,
        GGML_FTYPE_MOSTLY_IQ3_S = 20,
        GGML_FTYPE_MOSTLY_IQ2_S = 21,
        GGML_FTYPE_MOSTLY_IQ4_XS = 22,
        GGML_FTYPE_MOSTLY_IQ1_M = 23,
        GGML_FTYPE_MOSTLY_BF16 = 24,
        GGML_FTYPE_MOSTLY_MXFP4 = 25,
        GGML_FTYPE_MOSTLY_NVFP4 = 26,
        GGML_FTYPE_MOSTLY_Q1_0 = 27,
    }

    public enum ggml_op
    {
        GGML_OP_NONE = 0,
        GGML_OP_DUP,
        GGML_OP_ADD,
        GGML_OP_ADD_ID,
        GGML_OP_ADD1,
        GGML_OP_ACC,
        GGML_OP_SUB,
        GGML_OP_MUL,
        GGML_OP_DIV,
        GGML_OP_SQR,
        GGML_OP_SQRT,
        GGML_OP_LOG,
        GGML_OP_SIN,
        GGML_OP_COS,
        GGML_OP_SUM,
        GGML_OP_SUM_ROWS,
        GGML_OP_CUMSUM,
        GGML_OP_MEAN,
        GGML_OP_ARGMAX,
        GGML_OP_COUNT_EQUAL,
        GGML_OP_REPEAT,
        GGML_OP_REPEAT_BACK,
        GGML_OP_CONCAT,
        GGML_OP_SILU_BACK,
        GGML_OP_NORM,
        GGML_OP_RMS_NORM,
        GGML_OP_RMS_NORM_BACK,
        GGML_OP_GROUP_NORM,
        GGML_OP_L2_NORM,
        GGML_OP_MUL_MAT,
        GGML_OP_MUL_MAT_ID,
        GGML_OP_OUT_PROD,
        GGML_OP_SCALE,
        GGML_OP_SET,
        GGML_OP_CPY,
        GGML_OP_CONT,
        GGML_OP_RESHAPE,
        GGML_OP_VIEW,
        GGML_OP_PERMUTE,
        GGML_OP_TRANSPOSE,
        GGML_OP_GET_ROWS,
        GGML_OP_GET_ROWS_BACK,
        GGML_OP_SET_ROWS,
        GGML_OP_DIAG,
        GGML_OP_DIAG_MASK_INF,
        GGML_OP_DIAG_MASK_ZERO,
        GGML_OP_SOFT_MAX,
        GGML_OP_SOFT_MAX_BACK,
        GGML_OP_ROPE,
        GGML_OP_ROPE_BACK,
        GGML_OP_CLAMP,
        GGML_OP_CONV_TRANSPOSE_1D,
        GGML_OP_IM2COL,
        GGML_OP_IM2COL_BACK,
        GGML_OP_IM2COL_3D,
        GGML_OP_COL2IM_1D,
        GGML_OP_CONV_2D,
        GGML_OP_CONV_3D,
        GGML_OP_CONV_2D_DW,
        GGML_OP_CONV_TRANSPOSE_2D,
        GGML_OP_POOL_1D,
        GGML_OP_POOL_2D,
        GGML_OP_POOL_2D_BACK,
        GGML_OP_UPSCALE,
        GGML_OP_PAD,
        GGML_OP_PAD_REFLECT_1D,
        GGML_OP_ROLL,
        GGML_OP_ARANGE,
        GGML_OP_TIMESTEP_EMBEDDING,
        GGML_OP_ARGSORT,
        GGML_OP_TOP_K,
        GGML_OP_LEAKY_RELU,
        GGML_OP_TRI,
        GGML_OP_FILL,
        GGML_OP_FLASH_ATTN_EXT,
        GGML_OP_FLASH_ATTN_BACK,
        GGML_OP_SSM_CONV,
        GGML_OP_SSM_SCAN,
        GGML_OP_WIN_PART,
        GGML_OP_WIN_UNPART,
        GGML_OP_GET_REL_POS,
        GGML_OP_ADD_REL_POS,
        GGML_OP_RWKV_WKV6,
        GGML_OP_GATED_LINEAR_ATTN,
        GGML_OP_RWKV_WKV7,
        GGML_OP_SOLVE_TRI,
        GGML_OP_GATED_DELTA_NET,
        GGML_OP_UNARY,
        GGML_OP_MAP_CUSTOM1,
        GGML_OP_MAP_CUSTOM2,
        GGML_OP_MAP_CUSTOM3,
        GGML_OP_CUSTOM,
        GGML_OP_CROSS_ENTROPY_LOSS,
        GGML_OP_CROSS_ENTROPY_LOSS_BACK,
        GGML_OP_OPT_STEP_ADAMW,
        GGML_OP_OPT_STEP_SGD,
        GGML_OP_GLU,
        GGML_OP_COUNT,
    }

    public enum ggml_unary_op
    {
        GGML_UNARY_OP_ABS,
        GGML_UNARY_OP_SGN,
        GGML_UNARY_OP_NEG,
        GGML_UNARY_OP_STEP,
        GGML_UNARY_OP_TANH,
        GGML_UNARY_OP_ELU,
        GGML_UNARY_OP_RELU,
        GGML_UNARY_OP_SIGMOID,
        GGML_UNARY_OP_GELU,
        GGML_UNARY_OP_GELU_QUICK,
        GGML_UNARY_OP_SILU,
        GGML_UNARY_OP_HARDSWISH,
        GGML_UNARY_OP_HARDSIGMOID,
        GGML_UNARY_OP_EXP,
        GGML_UNARY_OP_EXPM1,
        GGML_UNARY_OP_SOFTPLUS,
        GGML_UNARY_OP_GELU_ERF,
        GGML_UNARY_OP_XIELU,
        GGML_UNARY_OP_FLOOR,
        GGML_UNARY_OP_CEIL,
        GGML_UNARY_OP_ROUND,
        GGML_UNARY_OP_TRUNC,
        GGML_UNARY_OP_COUNT,
    }

    public enum ggml_glu_op
    {
        GGML_GLU_OP_REGLU,
        GGML_GLU_OP_GEGLU,
        GGML_GLU_OP_SWIGLU,
        GGML_GLU_OP_SWIGLU_OAI,
        GGML_GLU_OP_GEGLU_ERF,
        GGML_GLU_OP_GEGLU_QUICK,
        GGML_GLU_OP_COUNT,
    }

    public enum ggml_object_type
    {
        GGML_OBJECT_TYPE_TENSOR,
        GGML_OBJECT_TYPE_GRAPH,
        GGML_OBJECT_TYPE_WORK_BUFFER,
    }

    public enum ggml_log_level
    {
        GGML_LOG_LEVEL_NONE = 0,
        GGML_LOG_LEVEL_DEBUG = 1,
        GGML_LOG_LEVEL_INFO = 2,
        GGML_LOG_LEVEL_WARN = 3,
        GGML_LOG_LEVEL_ERROR = 4,
        GGML_LOG_LEVEL_CONT = 5,
    }

    public enum ggml_tensor_flag
    {
        GGML_TENSOR_FLAG_INPUT = 1,
        GGML_TENSOR_FLAG_OUTPUT = 2,
        GGML_TENSOR_FLAG_PARAM = 4,
        GGML_TENSOR_FLAG_LOSS = 8,
        GGML_TENSOR_FLAG_COMPUTE = 16,
    }

    public enum ggml_tri_type
    {
        GGML_TRI_TYPE_UPPER_DIAG = 0,
        GGML_TRI_TYPE_UPPER = 1,
        GGML_TRI_TYPE_LOWER_DIAG = 2,
        GGML_TRI_TYPE_LOWER = 3,
    }

    public unsafe partial struct ggml_init_params
    {
        [NativeTypeName("size_t")]
        public nuint mem_size;

        public void* mem_buffer;

        [NativeTypeName("_Bool")]
        public bool no_alloc;
    }

    public unsafe partial struct ggml_tensor
    {
        [NativeTypeName("enum ggml_type")]
        public ggml_type type;

        [NativeTypeName("struct ggml_backend_buffer *")]
        public ggml_backend_buffer* buffer;

        [NativeTypeName("int64_t[4]")]
        public _ne_e__FixedBuffer ne;

        [NativeTypeName("size_t[4]")]
        public _nb_e__FixedBuffer nb;

        [NativeTypeName("enum ggml_op")]
        public ggml_op op;

        [NativeTypeName("int32_t[16]")]
        public _op_params_e__FixedBuffer op_params;

        [NativeTypeName("int32_t")]
        public int flags;

        [NativeTypeName("struct ggml_tensor *[10]")]
        public _src_e__FixedBuffer src;

        [NativeTypeName("struct ggml_tensor *")]
        public ggml_tensor* view_src;

        [NativeTypeName("size_t")]
        public nuint view_offs;

        public void* data;

        [NativeTypeName("char[64]")]
        public _name_e__FixedBuffer name;

        public void* extra;

        [NativeTypeName("char[8]")]
        public _padding_e__FixedBuffer padding;

        [InlineArray(4)]
        public partial struct _ne_e__FixedBuffer
        {
            public long e0;
        }

        [InlineArray(4)]
        public partial struct _nb_e__FixedBuffer
        {
            public nuint e0;
        }

        [InlineArray(16)]
        public partial struct _op_params_e__FixedBuffer
        {
            public int e0;
        }

        public unsafe partial struct _src_e__FixedBuffer
        {
            public ggml_tensor* e0;
            public ggml_tensor* e1;
            public ggml_tensor* e2;
            public ggml_tensor* e3;
            public ggml_tensor* e4;
            public ggml_tensor* e5;
            public ggml_tensor* e6;
            public ggml_tensor* e7;
            public ggml_tensor* e8;
            public ggml_tensor* e9;

            public ref ggml_tensor* this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    fixed (ggml_tensor** pThis = &e0)
                    {
                        return ref pThis[index];
                    }
                }
            }
        }

        [InlineArray(64)]
        public partial struct _name_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(8)]
        public partial struct _padding_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public enum ggml_op_pool
    {
        GGML_OP_POOL_MAX,
        GGML_OP_POOL_AVG,
        GGML_OP_POOL_COUNT,
    }

    public enum ggml_scale_mode
    {
        GGML_SCALE_MODE_NEAREST = 0,
        GGML_SCALE_MODE_BILINEAR = 1,
        GGML_SCALE_MODE_BICUBIC = 2,
        GGML_SCALE_MODE_COUNT,
    }

    public enum ggml_scale_flag
    {
        GGML_SCALE_FLAG_ALIGN_CORNERS = (1 << 8),
        GGML_SCALE_FLAG_ANTIALIAS = (1 << 9),
    }

    public enum ggml_sort_order
    {
        GGML_SORT_ORDER_ASC,
        GGML_SORT_ORDER_DESC,
    }

    public unsafe partial struct ggml_type_traits
    {
        [NativeTypeName("const char *")]
        public sbyte* type_name;

        [NativeTypeName("int64_t")]
        public long blck_size;

        [NativeTypeName("int64_t")]
        public long blck_size_interleave;

        [NativeTypeName("size_t")]
        public nuint type_size;

        [NativeTypeName("_Bool")]
        public bool is_quantized;

        [NativeTypeName("ggml_to_float_t")]
        public delegate* unmanaged[Cdecl]<void*, float*, long, void> to_float;

        [NativeTypeName("ggml_from_float_t")]
        public delegate* unmanaged[Cdecl]<float*, void*, long, void> from_float_ref;
    }

    public enum ggml_sched_priority
    {
        GGML_SCHED_PRIO_LOW = -1,
        GGML_SCHED_PRIO_NORMAL,
        GGML_SCHED_PRIO_MEDIUM,
        GGML_SCHED_PRIO_HIGH,
        GGML_SCHED_PRIO_REALTIME,
    }

    public partial struct ggml_threadpool_params
    {
        [NativeTypeName("_Bool[512]")]
        public _cpumask_e__FixedBuffer cpumask;

        public int n_threads;

        [NativeTypeName("enum ggml_sched_priority")]
        public ggml_sched_priority prio;

        [NativeTypeName("uint32_t")]
        public uint poll;

        [NativeTypeName("_Bool")]
        public bool strict_cpu;

        [NativeTypeName("_Bool")]
        public bool paused;

        [InlineArray(512)]
        public partial struct _cpumask_e__FixedBuffer
        {
            public bool e0;
        }
    }

    public enum ggml_backend_buffer_usage
    {
        GGML_BACKEND_BUFFER_USAGE_ANY = 0,
        GGML_BACKEND_BUFFER_USAGE_WEIGHTS = 1,
        GGML_BACKEND_BUFFER_USAGE_COMPUTE = 2,
    }

    public enum ggml_backend_dev_type
    {
        GGML_BACKEND_DEVICE_TYPE_CPU,
        GGML_BACKEND_DEVICE_TYPE_GPU,
        GGML_BACKEND_DEVICE_TYPE_IGPU,
        GGML_BACKEND_DEVICE_TYPE_ACCEL,
        GGML_BACKEND_DEVICE_TYPE_META,
    }

    public partial struct ggml_backend_dev_caps
    {
        [NativeTypeName("_Bool")]
        public bool async;

        [NativeTypeName("_Bool")]
        public bool host_buffer;

        [NativeTypeName("_Bool")]
        public bool buffer_from_host_ptr;

        [NativeTypeName("_Bool")]
        public bool events;
    }

    public unsafe partial struct ggml_backend_dev_props
    {
        [NativeTypeName("const char *")]
        public sbyte* name;

        [NativeTypeName("const char *")]
        public sbyte* description;

        [NativeTypeName("size_t")]
        public nuint memory_free;

        [NativeTypeName("size_t")]
        public nuint memory_total;

        [NativeTypeName("enum ggml_backend_dev_type")]
        public ggml_backend_dev_type type;

        [NativeTypeName("const char *")]
        public sbyte* device_id;

        [NativeTypeName("struct ggml_backend_dev_caps")]
        public ggml_backend_dev_caps caps;
    }

    public unsafe partial struct ggml_backend_feature
    {
        [NativeTypeName("const char *")]
        public sbyte* name;

        [NativeTypeName("const char *")]
        public sbyte* value;
    }

    public enum ggml_backend_meta_split_axis
    {
        GGML_BACKEND_SPLIT_AXIS_0 = 0,
        GGML_BACKEND_SPLIT_AXIS_1 = 1,
        GGML_BACKEND_SPLIT_AXIS_2 = 2,
        GGML_BACKEND_SPLIT_AXIS_3 = 3,
        GGML_BACKEND_SPLIT_AXIS_MIRRORED = 10,
        GGML_BACKEND_SPLIT_AXIS_PARTIAL = 11,
        GGML_BACKEND_SPLIT_AXIS_NONE = 98,
        GGML_BACKEND_SPLIT_AXIS_UNKNOWN = 99,
    }

    public partial struct ggml_backend_meta_split_state
    {
        [NativeTypeName("enum ggml_backend_meta_split_axis")]
        public ggml_backend_meta_split_axis axis;

        [NativeTypeName("int64_t[256]")]
        public _ne_e__FixedBuffer ne;

        [NativeTypeName("uint32_t[16]")]
        public _nr_e__FixedBuffer nr;

        [NativeTypeName("uint32_t")]
        public uint n_segments;

        [InlineArray(256)]
        public partial struct _ne_e__FixedBuffer
        {
            public long e0;
        }

        [InlineArray(16)]
        public partial struct _nr_e__FixedBuffer
        {
            public uint e0;
        }
    }

    public unsafe partial struct ggml_backend_graph_copy
    {
        [NativeTypeName("ggml_backend_buffer_t")]
        public ggml_backend_buffer* buffer;

        [NativeTypeName("struct ggml_context *")]
        public ggml_context* ctx_allocated;

        [NativeTypeName("struct ggml_context *")]
        public ggml_context* ctx_unallocated;

        [NativeTypeName("struct ggml_cgraph *")]
        public ggml_cgraph* graph;
    }

    public unsafe partial struct ggml_cplan
    {
        [NativeTypeName("size_t")]
        public nuint work_size;

        [NativeTypeName("uint8_t *")]
        public byte* work_data;

        public int n_threads;

        [NativeTypeName("struct ggml_threadpool *")]
        public ggml_threadpool* threadpool;

        [NativeTypeName("ggml_abort_callback")]
        public delegate* unmanaged[Cdecl]<void*, bool> abort_callback;

        public void* abort_callback_data;

        [NativeTypeName("_Bool")]
        public bool use_ref;
    }

    public enum ggml_numa_strategy
    {
        GGML_NUMA_STRATEGY_DISABLED = 0,
        GGML_NUMA_STRATEGY_DISTRIBUTE = 1,
        GGML_NUMA_STRATEGY_ISOLATE = 2,
        GGML_NUMA_STRATEGY_NUMACTL = 3,
        GGML_NUMA_STRATEGY_MIRROR = 4,
        GGML_NUMA_STRATEGY_COUNT,
    }

    public unsafe partial struct ggml_type_traits_cpu
    {
        [NativeTypeName("ggml_from_float_t")]
        public delegate* unmanaged[Cdecl]<float*, void*, long, void> from_float;

        [NativeTypeName("ggml_vec_dot_t")]
        public delegate* unmanaged[Cdecl]<int, float*, nuint, void*, nuint, void*, nuint, int, void> vec_dot;

        [NativeTypeName("enum ggml_type")]
        public ggml_type vec_dot_type;

        [NativeTypeName("int64_t")]
        public long nrows;
    }

    public enum ggml_opt_loss_type
    {
        GGML_OPT_LOSS_TYPE_MEAN,
        GGML_OPT_LOSS_TYPE_SUM,
        GGML_OPT_LOSS_TYPE_CROSS_ENTROPY,
        GGML_OPT_LOSS_TYPE_MEAN_SQUARED_ERROR,
    }

    public enum ggml_opt_build_type
    {
        GGML_OPT_BUILD_TYPE_FORWARD = 10,
        GGML_OPT_BUILD_TYPE_GRAD = 20,
        GGML_OPT_BUILD_TYPE_OPT = 30,
    }

    public enum ggml_opt_optimizer_type
    {
        GGML_OPT_OPTIMIZER_TYPE_ADAMW,
        GGML_OPT_OPTIMIZER_TYPE_SGD,
        GGML_OPT_OPTIMIZER_TYPE_COUNT,
    }

    public partial struct ggml_opt_optimizer_params
    {
        [NativeTypeName("__AnonymousRecord_ggml-opt_L86_C9")]
        public _adamw_e__Struct adamw;

        [NativeTypeName("__AnonymousRecord_ggml-opt_L93_C9")]
        public _sgd_e__Struct sgd;

        public partial struct _adamw_e__Struct
        {
            public float alpha;

            public float beta1;

            public float beta2;

            public float eps;

            public float wd;
        }

        public partial struct _sgd_e__Struct
        {
            public float alpha;

            public float wd;
        }
    }

    public unsafe partial struct ggml_opt_params
    {
        [NativeTypeName("ggml_backend_sched_t")]
        public ggml_backend_sched* backend_sched;

        [NativeTypeName("struct ggml_context *")]
        public ggml_context* ctx_compute;

        [NativeTypeName("struct ggml_tensor *")]
        public ggml_tensor* inputs;

        [NativeTypeName("struct ggml_tensor *")]
        public ggml_tensor* outputs;

        [NativeTypeName("enum ggml_opt_loss_type")]
        public ggml_opt_loss_type loss_type;

        [NativeTypeName("enum ggml_opt_build_type")]
        public ggml_opt_build_type build_type;

        [NativeTypeName("int32_t")]
        public int opt_period;

        [NativeTypeName("ggml_opt_get_optimizer_params")]
        public delegate* unmanaged[Cdecl]<void*, ggml_opt_optimizer_params> get_opt_pars;

        public void* get_opt_pars_ud;

        [NativeTypeName("enum ggml_opt_optimizer_type")]
        public ggml_opt_optimizer_type optimizer;
    }

    public enum gguf_type
    {
        GGUF_TYPE_UINT8 = 0,
        GGUF_TYPE_INT8 = 1,
        GGUF_TYPE_UINT16 = 2,
        GGUF_TYPE_INT16 = 3,
        GGUF_TYPE_UINT32 = 4,
        GGUF_TYPE_INT32 = 5,
        GGUF_TYPE_FLOAT32 = 6,
        GGUF_TYPE_BOOL = 7,
        GGUF_TYPE_STRING = 8,
        GGUF_TYPE_ARRAY = 9,
        GGUF_TYPE_UINT64 = 10,
        GGUF_TYPE_INT64 = 11,
        GGUF_TYPE_FLOAT64 = 12,
        GGUF_TYPE_COUNT,
    }

    public unsafe partial struct gguf_init_params
    {
        [NativeTypeName("_Bool")]
        public bool no_alloc;

        [NativeTypeName("struct ggml_context **")]
        public ggml_context** ctx;
    }

    public enum llama_vocab_type
    {
        LLAMA_VOCAB_TYPE_NONE = 0,
        LLAMA_VOCAB_TYPE_SPM = 1,
        LLAMA_VOCAB_TYPE_BPE = 2,
        LLAMA_VOCAB_TYPE_WPM = 3,
        LLAMA_VOCAB_TYPE_UGM = 4,
        LLAMA_VOCAB_TYPE_RWKV = 5,
        LLAMA_VOCAB_TYPE_PLAMO2 = 6,
    }

    public enum llama_rope_type
    {
        LLAMA_ROPE_TYPE_NONE = -1,
        LLAMA_ROPE_TYPE_NORM = 0,
        LLAMA_ROPE_TYPE_NEOX = 2,
        LLAMA_ROPE_TYPE_MROPE = 8,
        LLAMA_ROPE_TYPE_IMROPE = 40,
        LLAMA_ROPE_TYPE_VISION = 24,
    }

    public enum llama_token_type
    {
        LLAMA_TOKEN_TYPE_UNDEFINED = 0,
        LLAMA_TOKEN_TYPE_NORMAL = 1,
        LLAMA_TOKEN_TYPE_UNKNOWN = 2,
        LLAMA_TOKEN_TYPE_CONTROL = 3,
        LLAMA_TOKEN_TYPE_USER_DEFINED = 4,
        LLAMA_TOKEN_TYPE_UNUSED = 5,
        LLAMA_TOKEN_TYPE_BYTE = 6,
    }

    public enum llama_token_attr
    {
        LLAMA_TOKEN_ATTR_UNDEFINED = 0,
        LLAMA_TOKEN_ATTR_UNKNOWN = 1 << 0,
        LLAMA_TOKEN_ATTR_UNUSED = 1 << 1,
        LLAMA_TOKEN_ATTR_NORMAL = 1 << 2,
        LLAMA_TOKEN_ATTR_CONTROL = 1 << 3,
        LLAMA_TOKEN_ATTR_USER_DEFINED = 1 << 4,
        LLAMA_TOKEN_ATTR_BYTE = 1 << 5,
        LLAMA_TOKEN_ATTR_NORMALIZED = 1 << 6,
        LLAMA_TOKEN_ATTR_LSTRIP = 1 << 7,
        LLAMA_TOKEN_ATTR_RSTRIP = 1 << 8,
        LLAMA_TOKEN_ATTR_SINGLE_WORD = 1 << 9,
    }

    public enum llama_ftype
    {
        LLAMA_FTYPE_ALL_F32 = 0,
        LLAMA_FTYPE_MOSTLY_F16 = 1,
        LLAMA_FTYPE_MOSTLY_Q4_0 = 2,
        LLAMA_FTYPE_MOSTLY_Q4_1 = 3,
        LLAMA_FTYPE_MOSTLY_Q8_0 = 7,
        LLAMA_FTYPE_MOSTLY_Q5_0 = 8,
        LLAMA_FTYPE_MOSTLY_Q5_1 = 9,
        LLAMA_FTYPE_MOSTLY_Q2_K = 10,
        LLAMA_FTYPE_MOSTLY_Q3_K_S = 11,
        LLAMA_FTYPE_MOSTLY_Q3_K_M = 12,
        LLAMA_FTYPE_MOSTLY_Q3_K_L = 13,
        LLAMA_FTYPE_MOSTLY_Q4_K_S = 14,
        LLAMA_FTYPE_MOSTLY_Q4_K_M = 15,
        LLAMA_FTYPE_MOSTLY_Q5_K_S = 16,
        LLAMA_FTYPE_MOSTLY_Q5_K_M = 17,
        LLAMA_FTYPE_MOSTLY_Q6_K = 18,
        LLAMA_FTYPE_MOSTLY_IQ2_XXS = 19,
        LLAMA_FTYPE_MOSTLY_IQ2_XS = 20,
        LLAMA_FTYPE_MOSTLY_Q2_K_S = 21,
        LLAMA_FTYPE_MOSTLY_IQ3_XS = 22,
        LLAMA_FTYPE_MOSTLY_IQ3_XXS = 23,
        LLAMA_FTYPE_MOSTLY_IQ1_S = 24,
        LLAMA_FTYPE_MOSTLY_IQ4_NL = 25,
        LLAMA_FTYPE_MOSTLY_IQ3_S = 26,
        LLAMA_FTYPE_MOSTLY_IQ3_M = 27,
        LLAMA_FTYPE_MOSTLY_IQ2_S = 28,
        LLAMA_FTYPE_MOSTLY_IQ2_M = 29,
        LLAMA_FTYPE_MOSTLY_IQ4_XS = 30,
        LLAMA_FTYPE_MOSTLY_IQ1_M = 31,
        LLAMA_FTYPE_MOSTLY_BF16 = 32,
        LLAMA_FTYPE_MOSTLY_TQ1_0 = 36,
        LLAMA_FTYPE_MOSTLY_TQ2_0 = 37,
        LLAMA_FTYPE_MOSTLY_MXFP4_MOE = 38,
        LLAMA_FTYPE_MOSTLY_NVFP4 = 39,
        LLAMA_FTYPE_MOSTLY_Q1_0 = 40,
        LLAMA_FTYPE_GUESSED = 1024,
    }

    public enum llama_rope_scaling_type
    {
        LLAMA_ROPE_SCALING_TYPE_UNSPECIFIED = -1,
        LLAMA_ROPE_SCALING_TYPE_NONE = 0,
        LLAMA_ROPE_SCALING_TYPE_LINEAR = 1,
        LLAMA_ROPE_SCALING_TYPE_YARN = 2,
        LLAMA_ROPE_SCALING_TYPE_LONGROPE = 3,
        LLAMA_ROPE_SCALING_TYPE_MAX_VALUE = LLAMA_ROPE_SCALING_TYPE_LONGROPE,
    }

    public enum llama_pooling_type
    {
        LLAMA_POOLING_TYPE_UNSPECIFIED = -1,
        LLAMA_POOLING_TYPE_NONE = 0,
        LLAMA_POOLING_TYPE_MEAN = 1,
        LLAMA_POOLING_TYPE_CLS = 2,
        LLAMA_POOLING_TYPE_LAST = 3,
        LLAMA_POOLING_TYPE_RANK = 4,
    }

    public enum llama_attention_type
    {
        LLAMA_ATTENTION_TYPE_UNSPECIFIED = -1,
        LLAMA_ATTENTION_TYPE_CAUSAL = 0,
        LLAMA_ATTENTION_TYPE_NON_CAUSAL = 1,
    }

    public enum llama_flash_attn_type
    {
        LLAMA_FLASH_ATTN_TYPE_AUTO = -1,
        LLAMA_FLASH_ATTN_TYPE_DISABLED = 0,
        LLAMA_FLASH_ATTN_TYPE_ENABLED = 1,
    }

    public enum llama_split_mode
    {
        LLAMA_SPLIT_MODE_NONE = 0,
        LLAMA_SPLIT_MODE_LAYER = 1,
        LLAMA_SPLIT_MODE_ROW = 2,
        LLAMA_SPLIT_MODE_TENSOR = 3,
    }

    public enum llama_context_type
    {
        LLAMA_CONTEXT_TYPE_DEFAULT = 0,
        LLAMA_CONTEXT_TYPE_MTP = 1,
    }

    public partial struct llama_token_data
    {
        [NativeTypeName("llama_token")]
        public int id;

        public float logit;

        public float p;
    }

    public unsafe partial struct llama_token_data_array
    {
        public llama_token_data* data;

        [NativeTypeName("size_t")]
        public nuint size;

        [NativeTypeName("int64_t")]
        public long selected;

        [NativeTypeName("_Bool")]
        public bool sorted;
    }

    public unsafe partial struct llama_batch
    {
        [NativeTypeName("int32_t")]
        public int n_tokens;

        [NativeTypeName("llama_token *")]
        public int* token;

        public float* embd;

        [NativeTypeName("llama_pos *")]
        public int* pos;

        [NativeTypeName("int32_t *")]
        public int* n_seq_id;

        [NativeTypeName("llama_seq_id **")]
        public int** seq_id;

        [NativeTypeName("int8_t *")]
        public sbyte* logits;
    }

    public enum llama_model_kv_override_type
    {
        LLAMA_KV_OVERRIDE_TYPE_INT,
        LLAMA_KV_OVERRIDE_TYPE_FLOAT,
        LLAMA_KV_OVERRIDE_TYPE_BOOL,
        LLAMA_KV_OVERRIDE_TYPE_STR,
    }

    public enum llama_model_meta_key
    {
        LLAMA_MODEL_META_KEY_SAMPLING_SEQUENCE,
        LLAMA_MODEL_META_KEY_SAMPLING_TOP_K,
        LLAMA_MODEL_META_KEY_SAMPLING_TOP_P,
        LLAMA_MODEL_META_KEY_SAMPLING_MIN_P,
        LLAMA_MODEL_META_KEY_SAMPLING_XTC_PROBABILITY,
        LLAMA_MODEL_META_KEY_SAMPLING_XTC_THRESHOLD,
        LLAMA_MODEL_META_KEY_SAMPLING_TEMP,
        LLAMA_MODEL_META_KEY_SAMPLING_PENALTY_LAST_N,
        LLAMA_MODEL_META_KEY_SAMPLING_PENALTY_REPEAT,
        LLAMA_MODEL_META_KEY_SAMPLING_MIROSTAT,
        LLAMA_MODEL_META_KEY_SAMPLING_MIROSTAT_TAU,
        LLAMA_MODEL_META_KEY_SAMPLING_MIROSTAT_ETA,
    }

    public partial struct llama_model_kv_override
    {
        [NativeTypeName("enum llama_model_kv_override_type")]
        public llama_model_kv_override_type tag;

        [NativeTypeName("char[128]")]
        public _key_e__FixedBuffer key;

        [NativeTypeName("__AnonymousRecord_llama_L278_C9")]
        public _Anonymous_e__Union Anonymous;

        [UnscopedRef]
        public ref long val_i64
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref Anonymous.val_i64;
            }
        }

        [UnscopedRef]
        public ref double val_f64
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref Anonymous.val_f64;
            }
        }

        [UnscopedRef]
        public ref bool val_bool
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref Anonymous.val_bool;
            }
        }

        [UnscopedRef]
        public Span<sbyte> val_str
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Anonymous.val_str;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("int64_t")]
            public long val_i64;

            [FieldOffset(0)]
            public double val_f64;

            [FieldOffset(0)]
            [NativeTypeName("_Bool")]
            public bool val_bool;

            [FieldOffset(0)]
            [NativeTypeName("char[128]")]
            public _val_str_e__FixedBuffer val_str;

            [InlineArray(128)]
            public partial struct _val_str_e__FixedBuffer
            {
                public sbyte e0;
            }
        }

        [InlineArray(128)]
        public partial struct _key_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public unsafe partial struct llama_model_tensor_buft_override
    {
        [NativeTypeName("const char *")]
        public sbyte* pattern;

        [NativeTypeName("ggml_backend_buffer_type_t")]
        public ggml_backend_buffer_type* buft;
    }

    public unsafe partial struct llama_model_params
    {
        [NativeTypeName("ggml_backend_dev_t *")]
        public ggml_backend_device** devices;

        [NativeTypeName("const struct llama_model_tensor_buft_override *")]
        public llama_model_tensor_buft_override* tensor_buft_overrides;

        [NativeTypeName("int32_t")]
        public int n_gpu_layers;

        [NativeTypeName("enum llama_split_mode")]
        public llama_split_mode split_mode;

        [NativeTypeName("int32_t")]
        public int main_gpu;

        [NativeTypeName("const float *")]
        public float* tensor_split;

        [NativeTypeName("llama_progress_callback")]
        public delegate* unmanaged[Cdecl]<float, void*, bool> progress_callback;

        public void* progress_callback_user_data;

        [NativeTypeName("const struct llama_model_kv_override *")]
        public llama_model_kv_override* kv_overrides;

        [NativeTypeName("_Bool")]
        public bool vocab_only;

        [NativeTypeName("_Bool")]
        public bool use_mmap;

        [NativeTypeName("_Bool")]
        public bool use_direct_io;

        [NativeTypeName("_Bool")]
        public bool use_mlock;

        [NativeTypeName("_Bool")]
        public bool check_tensors;

        [NativeTypeName("_Bool")]
        public bool use_extra_bufts;

        [NativeTypeName("_Bool")]
        public bool no_host;

        [NativeTypeName("_Bool")]
        public bool no_alloc;
    }

    public unsafe partial struct llama_sampler_seq_config
    {
        [NativeTypeName("llama_seq_id")]
        public int seq_id;

        [NativeTypeName("struct llama_sampler *")]
        public llama_sampler* sampler;
    }

    public unsafe partial struct llama_context_params
    {
        [NativeTypeName("uint32_t")]
        public uint n_ctx;

        [NativeTypeName("uint32_t")]
        public uint n_batch;

        [NativeTypeName("uint32_t")]
        public uint n_ubatch;

        [NativeTypeName("uint32_t")]
        public uint n_seq_max;

        [NativeTypeName("uint32_t")]
        public uint n_rs_seq;

        [NativeTypeName("uint32_t")]
        public uint n_outputs_max;

        [NativeTypeName("int32_t")]
        public int n_threads;

        [NativeTypeName("int32_t")]
        public int n_threads_batch;

        [NativeTypeName("enum llama_context_type")]
        public llama_context_type ctx_type;

        [NativeTypeName("enum llama_rope_scaling_type")]
        public llama_rope_scaling_type rope_scaling_type;

        [NativeTypeName("enum llama_pooling_type")]
        public llama_pooling_type pooling_type;

        [NativeTypeName("enum llama_attention_type")]
        public llama_attention_type attention_type;

        [NativeTypeName("enum llama_flash_attn_type")]
        public llama_flash_attn_type flash_attn_type;

        public float rope_freq_base;

        public float rope_freq_scale;

        public float yarn_ext_factor;

        public float yarn_attn_factor;

        public float yarn_beta_fast;

        public float yarn_beta_slow;

        [NativeTypeName("uint32_t")]
        public uint yarn_orig_ctx;

        public float defrag_thold;

        [NativeTypeName("ggml_backend_sched_eval_callback")]
        public delegate* unmanaged[Cdecl]<ggml_tensor*, bool, void*, bool> cb_eval;

        public void* cb_eval_user_data;

        [NativeTypeName("enum ggml_type")]
        public ggml_type type_k;

        [NativeTypeName("enum ggml_type")]
        public ggml_type type_v;

        [NativeTypeName("ggml_abort_callback")]
        public delegate* unmanaged[Cdecl]<void*, bool> abort_callback;

        public void* abort_callback_data;

        [NativeTypeName("_Bool")]
        public bool embeddings;

        [NativeTypeName("_Bool")]
        public bool offload_kqv;

        [NativeTypeName("_Bool")]
        public bool no_perf;

        [NativeTypeName("_Bool")]
        public bool op_offload;

        [NativeTypeName("_Bool")]
        public bool swa_full;

        [NativeTypeName("_Bool")]
        public bool kv_unified;

        [NativeTypeName("struct llama_sampler_seq_config *")]
        public llama_sampler_seq_config* samplers;

        [NativeTypeName("size_t")]
        public nuint n_samplers;

        [NativeTypeName("struct llama_context *")]
        public llama_context* ctx_other;
    }

    public unsafe partial struct llama_model_tensor_override
    {
        [NativeTypeName("const char *")]
        public sbyte* pattern;

        [NativeTypeName("enum ggml_type")]
        public ggml_type type;
    }

    public unsafe partial struct llama_model_imatrix_data
    {
        [NativeTypeName("const char *")]
        public sbyte* name;

        [NativeTypeName("const float *")]
        public float* data;

        [NativeTypeName("size_t")]
        public nuint size;
    }

    public unsafe partial struct llama_model_quantize_params
    {
        [NativeTypeName("int32_t")]
        public int nthread;

        [NativeTypeName("enum llama_ftype")]
        public llama_ftype ftype;

        [NativeTypeName("enum ggml_type")]
        public ggml_type output_tensor_type;

        [NativeTypeName("enum ggml_type")]
        public ggml_type token_embedding_type;

        [NativeTypeName("_Bool")]
        public bool allow_requantize;

        [NativeTypeName("_Bool")]
        public bool quantize_output_tensor;

        [NativeTypeName("_Bool")]
        public bool only_copy;

        [NativeTypeName("_Bool")]
        public bool pure;

        [NativeTypeName("_Bool")]
        public bool keep_split;

        [NativeTypeName("_Bool")]
        public bool dry_run;

        [NativeTypeName("const struct llama_model_imatrix_data *")]
        public llama_model_imatrix_data* imatrix;

        [NativeTypeName("const struct llama_model_kv_override *")]
        public llama_model_kv_override* kv_overrides;

        [NativeTypeName("const struct llama_model_tensor_override *")]
        public llama_model_tensor_override* tt_overrides;

        [NativeTypeName("const int32_t *")]
        public int* prune_layers;
    }

    public partial struct llama_logit_bias
    {
        [NativeTypeName("llama_token")]
        public int token;

        public float bias;
    }

    public partial struct llama_sampler_chain_params
    {
        [NativeTypeName("_Bool")]
        public bool no_perf;
    }

    public unsafe partial struct llama_chat_message
    {
        [NativeTypeName("const char *")]
        public sbyte* role;

        [NativeTypeName("const char *")]
        public sbyte* content;
    }

    public unsafe partial struct llama_sampler_data
    {
        [NativeTypeName("struct ggml_tensor *")]
        public ggml_tensor* logits;

        [NativeTypeName("struct ggml_tensor *")]
        public ggml_tensor* probs;

        [NativeTypeName("struct ggml_tensor *")]
        public ggml_tensor* sampled;

        [NativeTypeName("struct ggml_tensor *")]
        public ggml_tensor* candidates;
    }

    public unsafe partial struct llama_sampler_i
    {
        [NativeTypeName("const char *(*)(const struct llama_sampler *)")]
        public delegate* unmanaged[Cdecl]<llama_sampler*, sbyte*> name;

        [NativeTypeName("void (*)(struct llama_sampler *, llama_token)")]
        public delegate* unmanaged[Cdecl]<llama_sampler*, int, void> accept;

        [NativeTypeName("void (*)(struct llama_sampler *, llama_token_data_array *)")]
        public delegate* unmanaged[Cdecl]<llama_sampler*, llama_token_data_array*, void> apply;

        [NativeTypeName("void (*)(struct llama_sampler *)")]
        public delegate* unmanaged[Cdecl]<llama_sampler*, void> reset;

        [NativeTypeName("struct llama_sampler *(*)(const struct llama_sampler *)")]
        public delegate* unmanaged[Cdecl]<llama_sampler*, llama_sampler*> clone;

        [NativeTypeName("void (*)(struct llama_sampler *)")]
        public delegate* unmanaged[Cdecl]<llama_sampler*, void> free;

        [NativeTypeName("_Bool (*)(struct llama_sampler *, ggml_backend_buffer_type_t)")]
        public delegate* unmanaged[Cdecl]<llama_sampler*, ggml_backend_buffer_type*, bool> backend_init;

        [NativeTypeName("void (*)(struct llama_sampler *, struct ggml_context *, struct ggml_cgraph *, struct ggml_tensor *)")]
        public delegate* unmanaged[Cdecl]<llama_sampler*, ggml_context*, ggml_cgraph*, ggml_tensor*, void> backend_accept;

        [NativeTypeName("void (*)(struct llama_sampler *, struct ggml_context *, struct ggml_cgraph *, struct llama_sampler_data *)")]
        public delegate* unmanaged[Cdecl]<llama_sampler*, ggml_context*, ggml_cgraph*, llama_sampler_data*, void> backend_apply;

        [NativeTypeName("void (*)(struct llama_sampler *)")]
        public delegate* unmanaged[Cdecl]<llama_sampler*, void> backend_set_input;
    }

    public unsafe partial struct llama_sampler
    {
        [NativeTypeName("struct llama_sampler_i *")]
        public llama_sampler_i* iface;

        [NativeTypeName("llama_sampler_context_t")]
        public void* ctx;
    }

    public partial struct llama_perf_context_data
    {
        public double t_start_ms;

        public double t_load_ms;

        public double t_p_eval_ms;

        public double t_eval_ms;

        [NativeTypeName("int32_t")]
        public int n_p_eval;

        [NativeTypeName("int32_t")]
        public int n_eval;

        [NativeTypeName("int32_t")]
        public int n_reused;
    }

    public partial struct llama_perf_sampler_data
    {
        public double t_sample_ms;

        [NativeTypeName("int32_t")]
        public int n_sample;
    }

    public unsafe partial struct llama_opt_params
    {
        [NativeTypeName("uint32_t")]
        public uint n_ctx_train;

        [NativeTypeName("llama_opt_param_filter")]
        public delegate* unmanaged[Cdecl]<ggml_tensor*, void*, bool> param_filter;

        public void* param_filter_ud;

        [NativeTypeName("ggml_opt_get_optimizer_params")]
        public delegate* unmanaged[Cdecl]<void*, ggml_opt_optimizer_params> get_opt_pars;

        public void* get_opt_pars_ud;

        [NativeTypeName("enum ggml_opt_optimizer_type")]
        public ggml_opt_optimizer_type optimizer_type;
    }

    public enum mtmd_input_chunk_type
    {
        MTMD_INPUT_CHUNK_TYPE_TEXT,
        MTMD_INPUT_CHUNK_TYPE_IMAGE,
        MTMD_INPUT_CHUNK_TYPE_AUDIO,
    }

    public unsafe partial struct mtmd_input_text
    {
        [NativeTypeName("const char *")]
        public sbyte* text;

        [NativeTypeName("_Bool")]
        public bool add_special;

        [NativeTypeName("_Bool")]
        public bool parse_special;
    }

    public unsafe partial struct mtmd_context_params
    {
        [NativeTypeName("_Bool")]
        public bool use_gpu;

        [NativeTypeName("_Bool")]
        public bool print_timings;

        public int n_threads;

        [NativeTypeName("const char *")]
        public sbyte* image_marker;

        [NativeTypeName("const char *")]
        public sbyte* media_marker;

        [NativeTypeName("enum llama_flash_attn_type")]
        public llama_flash_attn_type flash_attn_type;

        [NativeTypeName("_Bool")]
        public bool warmup;

        public int image_min_tokens;

        public int image_max_tokens;

        [NativeTypeName("ggml_backend_sched_eval_callback")]
        public delegate* unmanaged[Cdecl]<ggml_tensor*, bool, void*, bool> cb_eval;

        public void* cb_eval_user_data;
    }

    public partial struct mtmd_decoder_pos
    {
        [NativeTypeName("uint32_t")]
        public uint t;

        [NativeTypeName("uint32_t")]
        public uint x;

        [NativeTypeName("uint32_t")]
        public uint y;

        [NativeTypeName("uint32_t")]
        public uint z;
    }

    public partial struct mtmd_caps
    {
        [NativeTypeName("_Bool")]
        public bool inp_vision;

        [NativeTypeName("_Bool")]
        public bool inp_audio;
    }

    public unsafe partial struct mtmd_helper_bitmap_wrapper
    {
        public mtmd_bitmap* bitmap;

        public mtmd_helper_video* video_ctx;
    }

    public partial struct mtmd_helper_video_info
    {
        [NativeTypeName("uint32_t")]
        public uint width;

        [NativeTypeName("uint32_t")]
        public uint height;

        public float fps;

        [NativeTypeName("int32_t")]
        public int n_frames;
    }

    public unsafe partial struct mtmd_helper_video_init_params
    {
        public float fps_target;

        [NativeTypeName("const char *")]
        public sbyte* ffmpeg_bin_dir;

        [NativeTypeName("int64_t")]
        public long timestamp_interval_ms;
    }

    public static unsafe partial class Methods
    {
        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_abort_callback_t")]
        public static extern delegate* unmanaged[Cdecl]<sbyte*, void> ggml_set_abort_callback([NativeTypeName("ggml_abort_callback_t")] delegate* unmanaged[Cdecl]<sbyte*, void> callback);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_status_to_string([NativeTypeName("enum ggml_status")] ggml_status status);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float ggml_fp16_to_fp32([NativeTypeName("ggml_fp16_t")] ushort param0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_fp16_t")]
        public static extern ushort ggml_fp32_to_fp16(float param0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_fp16_to_fp32_row([NativeTypeName("const ggml_fp16_t *")] ushort* param0, float* param1, [NativeTypeName("int64_t")] long param2);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_fp32_to_fp16_row([NativeTypeName("const float *")] float* param0, [NativeTypeName("ggml_fp16_t *")] ushort* param1, [NativeTypeName("int64_t")] long param2);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ggml_bf16_t ggml_fp32_to_bf16(float param0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float ggml_bf16_to_fp32(ggml_bf16_t param0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_bf16_to_fp32_row([NativeTypeName("const ggml_bf16_t *")] ggml_bf16_t* param0, float* param1, [NativeTypeName("int64_t")] long param2);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_fp32_to_bf16_row_ref([NativeTypeName("const float *")] float* param0, ggml_bf16_t* param1, [NativeTypeName("int64_t")] long param2);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_fp32_to_bf16_row([NativeTypeName("const float *")] float* param0, ggml_bf16_t* param1, [NativeTypeName("int64_t")] long param2);

        [NativeTypeName("const size_t")]
        public static nuint GGML_TENSOR_SIZE => unchecked((nuint)(sizeof(ggml_tensor)));

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_guid_matches([NativeTypeName("ggml_guid_t")] byte** guid_a, [NativeTypeName("ggml_guid_t")] byte** guid_b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_version();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_commit();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_time_init();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long ggml_time_ms();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long ggml_time_us();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long ggml_cycles();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long ggml_cycles_per_ms();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("FILE *")]
        public static extern _iobuf* ggml_fopen([NativeTypeName("const char *")] sbyte* fname, [NativeTypeName("const char *")] sbyte* mode);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_print_object([NativeTypeName("const struct ggml_object *")] ggml_object* obj);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_print_objects([NativeTypeName("const struct ggml_context *")] ggml_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long ggml_nelements([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long ggml_nrows([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_nbytes([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_nbytes_pad([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long ggml_blck_size([NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_type_size([NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_row_size([NativeTypeName("enum ggml_type")] ggml_type type, [NativeTypeName("int64_t")] long ne);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [Obsolete("use ggml_row_size() instead")]
        public static extern double ggml_type_sizef([NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_type_name([NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_op_name([NativeTypeName("enum ggml_op")] ggml_op op);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_op_symbol([NativeTypeName("enum ggml_op")] ggml_op op);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_unary_op_name([NativeTypeName("enum ggml_unary_op")] ggml_unary_op op);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_glu_op_name([NativeTypeName("enum ggml_glu_op")] ggml_glu_op op);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_op_desc([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* t);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_element_size([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_quantized([NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_type")]
        public static extern ggml_type ggml_ftype_to_ggml_type([NativeTypeName("enum ggml_ftype")] ggml_ftype ftype);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_transposed([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_permuted([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_empty([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_view([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_scalar([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_vector([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_matrix([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_3d([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_n_dims([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_contiguous([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_contiguous_0([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_contiguous_1([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_contiguous_2([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_contiguously_allocated([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_contiguous_channels([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_contiguous_rows([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_are_same_shape([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* t0, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* t1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_are_same_stride([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* t0, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* t1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_can_repeat([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* t0, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* t1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_tensor_overhead();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_validate_row_data([NativeTypeName("enum ggml_type")] ggml_type type, [NativeTypeName("const void *")] void* data, [NativeTypeName("size_t")] nuint nbytes);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_context *")]
        public static extern ggml_context* ggml_init([NativeTypeName("struct ggml_init_params")] ggml_init_params @params);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_reset([NativeTypeName("struct ggml_context *")] ggml_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_free([NativeTypeName("struct ggml_context *")] ggml_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_used_mem([NativeTypeName("const struct ggml_context *")] ggml_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_get_no_alloc([NativeTypeName("struct ggml_context *")] ggml_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_set_no_alloc([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("_Bool")] bool no_alloc);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void* ggml_get_mem_buffer([NativeTypeName("const struct ggml_context *")] ggml_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_get_mem_size([NativeTypeName("const struct ggml_context *")] ggml_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_get_max_tensor_size([NativeTypeName("const struct ggml_context *")] ggml_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_new_tensor([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("enum ggml_type")] ggml_type type, int n_dims, [NativeTypeName("const int64_t *")] long* ne);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_new_tensor_1d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("enum ggml_type")] ggml_type type, [NativeTypeName("int64_t")] long ne0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_new_tensor_2d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("enum ggml_type")] ggml_type type, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_new_tensor_3d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("enum ggml_type")] ggml_type type, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("int64_t")] long ne2);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_new_tensor_4d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("enum ggml_type")] ggml_type type, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("int64_t")] long ne2, [NativeTypeName("int64_t")] long ne3);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void* ggml_new_buffer([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("size_t")] nuint nbytes);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_dup_tensor([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* src);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_view_tensor([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* src);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_get_first_tensor([NativeTypeName("const struct ggml_context *")] ggml_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_get_next_tensor([NativeTypeName("const struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_get_tensor([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_unravel_index([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, [NativeTypeName("int64_t")] long i, [NativeTypeName("int64_t *")] long* i0, [NativeTypeName("int64_t *")] long* i1, [NativeTypeName("int64_t *")] long* i2, [NativeTypeName("int64_t *")] long* i3);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_unary_op")]
        public static extern ggml_unary_op ggml_get_unary_op([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_glu_op")]
        public static extern ggml_glu_op ggml_get_glu_op([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void* ggml_get_data([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float* ggml_get_data_f32([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_get_name([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_set_name([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_set_input([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_set_output([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_set_param([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_set_loss([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_dup([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_dup_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_add([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_add_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_add_cast([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_add_id([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* ids);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        [Obsolete("use ggml_add instead")]
        public static extern ggml_tensor* ggml_add1([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        [Obsolete("use ggml_add_inplace instead")]
        public static extern ggml_tensor* ggml_add1_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_acc([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("size_t")] nuint nb1, [NativeTypeName("size_t")] nuint nb2, [NativeTypeName("size_t")] nuint nb3, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_acc_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("size_t")] nuint nb1, [NativeTypeName("size_t")] nuint nb2, [NativeTypeName("size_t")] nuint nb3, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sub([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sub_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_mul([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_mul_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_div([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_div_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sqr([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sqr_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sqrt([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sqrt_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_log([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_log_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_expm1([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_expm1_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_softplus([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_softplus_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sin([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sin_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cos([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cos_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sum([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sum_rows([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cumsum([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_mean([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_argmax([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_count_equal([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_repeat([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_repeat_4d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("int64_t")] long ne2, [NativeTypeName("int64_t")] long ne3);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_repeat_back([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_concat([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int dim);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_abs([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_abs_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sgn([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sgn_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_neg([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_neg_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_step([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_step_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_tanh([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_tanh_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_elu([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_elu_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_relu([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_leaky_relu([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float negative_slope, [NativeTypeName("_Bool")] bool inplace);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_relu_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sigmoid([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_sigmoid_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_gelu([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_gelu_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_gelu_erf([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_gelu_erf_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_gelu_quick([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_gelu_quick_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_silu([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_silu_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_silu_back([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_hardswish([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_hardsigmoid([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_exp([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_exp_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_floor([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_floor_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_ceil([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_ceil_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_round([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_round_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_trunc([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_trunc_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_xielu([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float alpha_n, float alpha_p, float beta, float eps);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_glu([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("enum ggml_glu_op")] ggml_glu_op op, [NativeTypeName("_Bool")] bool swapped);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_reglu([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_reglu_swapped([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_geglu([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_geglu_swapped([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_swiglu([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_swiglu_swapped([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_geglu_erf([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_geglu_erf_swapped([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_geglu_quick([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_geglu_quick_swapped([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_glu_split([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("enum ggml_glu_op")] ggml_glu_op op);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_reglu_split([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_geglu_split([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_swiglu_split([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_geglu_erf_split([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_geglu_quick_split([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_swiglu_oai([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, float alpha, float limit);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_norm([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float eps);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_norm_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float eps);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rms_norm([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float eps);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rms_norm_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float eps);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_group_norm([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int n_groups, float eps);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_group_norm_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int n_groups, float eps);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_l2_norm([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float eps);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_l2_norm_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float eps);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rms_norm_back([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, float eps);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_mul_mat([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_mul_mat_set_prec([NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("enum ggml_prec")] ggml_prec prec);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_mul_mat_set_hint([NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("enum ggml_op_hint")] ggml_op_hint hint);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_mul_mat_id([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* @as, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* ids);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_out_prod([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_scale([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float s);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_scale_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float s);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_scale_bias([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float s, float b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_scale_bias_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float s, float b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_set([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("size_t")] nuint nb1, [NativeTypeName("size_t")] nuint nb2, [NativeTypeName("size_t")] nuint nb3, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_set_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("size_t")] nuint nb1, [NativeTypeName("size_t")] nuint nb2, [NativeTypeName("size_t")] nuint nb3, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_set_1d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_set_1d_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_set_2d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("size_t")] nuint nb1, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_set_2d_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("size_t")] nuint nb1, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cpy([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cast([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cont([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cont_1d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cont_2d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cont_3d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("int64_t")] long ne2);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cont_4d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("int64_t")] long ne2, [NativeTypeName("int64_t")] long ne3);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_reshape([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_reshape_1d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_reshape_2d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_reshape_3d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("int64_t")] long ne2);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_reshape_4d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("int64_t")] long ne2, [NativeTypeName("int64_t")] long ne3);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_view_1d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_view_2d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("size_t")] nuint nb1, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_view_3d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("int64_t")] long ne2, [NativeTypeName("size_t")] nuint nb1, [NativeTypeName("size_t")] nuint nb2, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_view_4d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("int64_t")] long ne2, [NativeTypeName("int64_t")] long ne3, [NativeTypeName("size_t")] nuint nb1, [NativeTypeName("size_t")] nuint nb2, [NativeTypeName("size_t")] nuint nb3, [NativeTypeName("size_t")] nuint offset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_permute([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int axis0, int axis1, int axis2, int axis3);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_transpose([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_get_rows([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_get_rows_back([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_set_rows([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_diag([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_diag_mask_inf([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int n_past);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_diag_mask_inf_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int n_past);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_diag_mask_zero([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int n_past);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_diag_mask_zero_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int n_past);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_soft_max([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_soft_max_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_soft_max_ext([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* mask, float scale, float max_bias);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_soft_max_ext_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* mask, float scale, float max_bias);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_soft_max_add_sinks([NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* sinks);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_soft_max_ext_back([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, float scale, float max_bias);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_soft_max_ext_back_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, float scale, float max_bias);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rope([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int n_dims, int mode);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rope_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int n_dims, int mode);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rope_ext([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c, int n_dims, int mode, int n_ctx_orig, float freq_base, float freq_scale, float ext_factor, float attn_factor, float beta_fast, float beta_slow);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rope_multi([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c, int n_dims, [NativeTypeName("int[4]")] int* sections, int mode, int n_ctx_orig, float freq_base, float freq_scale, float ext_factor, float attn_factor, float beta_fast, float beta_slow);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rope_ext_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c, int n_dims, int mode, int n_ctx_orig, float freq_base, float freq_scale, float ext_factor, float attn_factor, float beta_fast, float beta_slow);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rope_multi_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c, int n_dims, [NativeTypeName("int[4]")] int* sections, int mode, int n_ctx_orig, float freq_base, float freq_scale, float ext_factor, float attn_factor, float beta_fast, float beta_slow);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        [Obsolete("use ggml_rope_ext instead")]
        public static extern ggml_tensor* ggml_rope_custom([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int n_dims, int mode, int n_ctx_orig, float freq_base, float freq_scale, float ext_factor, float attn_factor, float beta_fast, float beta_slow);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        [Obsolete("use ggml_rope_ext_inplace instead")]
        public static extern ggml_tensor* ggml_rope_custom_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int n_dims, int mode, int n_ctx_orig, float freq_base, float freq_scale, float ext_factor, float attn_factor, float beta_fast, float beta_slow);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_rope_yarn_corr_dims(int n_dims, int n_ctx_orig, float freq_base, float beta_fast, float beta_slow, [NativeTypeName("float[2]")] float* dims);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rope_ext_back([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c, int n_dims, int mode, int n_ctx_orig, float freq_base, float freq_scale, float ext_factor, float attn_factor, float beta_fast, float beta_slow);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rope_multi_back([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c, int n_dims, [NativeTypeName("int[4]")] int* sections, int mode, int n_ctx_orig, float freq_base, float freq_scale, float ext_factor, float attn_factor, float beta_fast, float beta_slow);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_clamp([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float min, float max);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_im2col([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int s0, int s1, int p0, int p1, int d0, int d1, [NativeTypeName("_Bool")] bool is_2D, [NativeTypeName("enum ggml_type")] ggml_type dst_type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_im2col_back([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("int64_t *")] long* ne, int s0, int s1, int p0, int p1, int d0, int d1, [NativeTypeName("_Bool")] bool is_2D);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_col2im_1d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int s0, int oc, int p0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_1d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int s0, int p0, int d0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_1d_ph([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int s, int d);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_1d_dw([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int s0, int p0, int d0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_1d_dw_ph([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int s0, int d0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_transpose_1d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int s0, int p0, int d0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_2d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int s0, int s1, int p0, int p1, int d0, int d1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_im2col_3d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("int64_t")] long IC, int s0, int s1, int s2, int p0, int p1, int p2, int d0, int d1, int d2, [NativeTypeName("enum ggml_type")] ggml_type dst_type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_3d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("int64_t")] long IC, int s0, int s1, int s2, int p0, int p1, int p2, int d0, int d1, int d2);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_2d_sk_p0([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_2d_s1_ph([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_2d_dw([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int s0, int s1, int p0, int p1, int d0, int d1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_2d_dw_direct([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int stride0, int stride1, int pad0, int pad1, int dilation0, int dilation1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_transpose_2d_p0([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int stride);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_2d_direct([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int s0, int s1, int p0, int p1, int d0, int d1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_conv_3d_direct([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, int s0, int s1, int s2, int p0, int p1, int p2, int d0, int d1, int d2, int n_channels, int n_batch, int n_channels_out);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_pool_1d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("enum ggml_op_pool")] ggml_op_pool op, int k0, int s0, int p0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_pool_2d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("enum ggml_op_pool")] ggml_op_pool op, int k0, int k1, int s0, int s1, float p0, float p1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_pool_2d_back([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* af, [NativeTypeName("enum ggml_op_pool")] ggml_op_pool op, int k0, int k1, int s0, int s1, float p0, float p1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_upscale([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int scale_factor, [NativeTypeName("enum ggml_scale_mode")] ggml_scale_mode mode);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        [Obsolete("use ggml_interpolate instead")]
        public static extern ggml_tensor* ggml_upscale_ext([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int ne0, int ne1, int ne2, int ne3, [NativeTypeName("enum ggml_scale_mode")] ggml_scale_mode mode);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_interpolate([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("int64_t")] long ne2, [NativeTypeName("int64_t")] long ne3, [NativeTypeName("uint32_t")] uint mode);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_pad([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int p0, int p1, int p2, int p3);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_pad_circular([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int p0, int p1, int p2, int p3);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_pad_ext([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int lp0, int rp0, int lp1, int rp1, int lp2, int rp2, int lp3, int rp3);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_pad_ext_circular([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int lp0, int rp0, int lp1, int rp1, int lp2, int rp2, int lp3, int rp3);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_pad_reflect_1d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int p0, int p1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_roll([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int shift0, int shift1, int shift2, int shift3);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_tri([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("enum ggml_tri_type")] ggml_tri_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_fill([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float c);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_fill_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, float c);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_timestep_embedding([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* timesteps, int dim, int max_period);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_argsort([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("enum ggml_sort_order")] ggml_sort_order order);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_argsort_top_k([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int k);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_top_k([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int k);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_arange([NativeTypeName("struct ggml_context *")] ggml_context* ctx, float start, float stop, float step);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_flash_attn_ext([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* q, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* k, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* v, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* mask, float scale, float max_bias, float logit_softcap);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_flash_attn_ext_set_prec([NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("enum ggml_prec")] ggml_prec prec);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_prec")]
        public static extern ggml_prec ggml_flash_attn_ext_get_prec([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* a);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_flash_attn_ext_add_sinks([NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* sinks);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_flash_attn_back([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* q, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* k, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* v, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* d, [NativeTypeName("_Bool")] bool masked);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_ssm_conv([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* sx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_ssm_scan([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* s, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* x, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* dt, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* A, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* B, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* C, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* ids);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_win_part([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int w);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_win_unpart([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int w0, int h0, int w);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_unary([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("enum ggml_unary_op")] ggml_unary_op op);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_unary_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("enum ggml_unary_op")] ggml_unary_op op);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_get_rel_pos([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, int qh, int kh);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_add_rel_pos([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* pw, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* ph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_add_rel_pos_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* pw, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* ph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rwkv_wkv6([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* k, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* v, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* r, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* tf, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* td, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* state);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_gated_linear_attn([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* k, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* v, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* q, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* g, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* state, float scale);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_rwkv_wkv7([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* r, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* w, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* k, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* v, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* state);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_solve_tri([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("_Bool")] bool left, [NativeTypeName("_Bool")] bool lower, [NativeTypeName("_Bool")] bool uni);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_gated_delta_net([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* q, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* k, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* v, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* g, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* beta, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* state, [NativeTypeName("int64_t")] long K);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_map_custom1([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("ggml_custom1_op_t")] delegate* unmanaged[Cdecl]<ggml_tensor*, ggml_tensor*, int, int, void*, void> fun, int n_tasks, void* userdata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_map_custom1_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("ggml_custom1_op_t")] delegate* unmanaged[Cdecl]<ggml_tensor*, ggml_tensor*, int, int, void*, void> fun, int n_tasks, void* userdata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_map_custom2([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("ggml_custom2_op_t")] delegate* unmanaged[Cdecl]<ggml_tensor*, ggml_tensor*, ggml_tensor*, int, int, void*, void> fun, int n_tasks, void* userdata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_map_custom2_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("ggml_custom2_op_t")] delegate* unmanaged[Cdecl]<ggml_tensor*, ggml_tensor*, ggml_tensor*, int, int, void*, void> fun, int n_tasks, void* userdata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_map_custom3([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c, [NativeTypeName("ggml_custom3_op_t")] delegate* unmanaged[Cdecl]<ggml_tensor*, ggml_tensor*, ggml_tensor*, ggml_tensor*, int, int, void*, void> fun, int n_tasks, void* userdata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_map_custom3_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c, [NativeTypeName("ggml_custom3_op_t")] delegate* unmanaged[Cdecl]<ggml_tensor*, ggml_tensor*, ggml_tensor*, ggml_tensor*, int, int, void*, void> fun, int n_tasks, void* userdata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_custom_4d([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("enum ggml_type")] ggml_type type, [NativeTypeName("int64_t")] long ne0, [NativeTypeName("int64_t")] long ne1, [NativeTypeName("int64_t")] long ne2, [NativeTypeName("int64_t")] long ne3, [NativeTypeName("struct ggml_tensor **")] ggml_tensor** args, int n_args, [NativeTypeName("ggml_custom_op_t")] delegate* unmanaged[Cdecl]<ggml_tensor*, int, int, void*, void> fun, int n_tasks, void* userdata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_custom_inplace([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor **")] ggml_tensor** args, int n_args, [NativeTypeName("ggml_custom_op_t")] delegate* unmanaged[Cdecl]<ggml_tensor*, int, int, void*, void> fun, int n_tasks, void* userdata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cross_entropy_loss([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_cross_entropy_loss_back([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* b, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* c);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_opt_step_adamw([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* grad, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* m, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* v, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* adamw_params);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_opt_step_sgd([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* a, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* grad, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* sgd_params);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_build_forward_select([NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph, [NativeTypeName("struct ggml_tensor **")] ggml_tensor** tensors, int n_tensors, int idx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_build_forward_expand([NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_build_backward_expand([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph, [NativeTypeName("struct ggml_tensor **")] ggml_tensor** grad_accs);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_cgraph *")]
        public static extern ggml_cgraph* ggml_new_graph([NativeTypeName("struct ggml_context *")] ggml_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_cgraph *")]
        public static extern ggml_cgraph* ggml_new_graph_custom([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("size_t")] nuint size, [NativeTypeName("_Bool")] bool grads);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_cgraph *")]
        public static extern ggml_cgraph* ggml_graph_dup([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph, [NativeTypeName("_Bool")] bool force_grads);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_graph_cpy([NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* src, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* dst);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_graph_reset([NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_graph_clear([NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_graph_size([NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_graph_node([NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph, int i);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor **")]
        public static extern ggml_tensor** ggml_graph_nodes([NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_graph_n_nodes([NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_graph_add_node([NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_graph_overhead();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_graph_overhead_custom([NativeTypeName("size_t")] nuint size, [NativeTypeName("_Bool")] bool grads);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_graph_get_tensor([NativeTypeName("const struct ggml_cgraph *")] ggml_cgraph* cgraph, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_graph_get_grad([NativeTypeName("const struct ggml_cgraph *")] ggml_cgraph* cgraph, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* node);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_graph_get_grad_acc([NativeTypeName("const struct ggml_cgraph *")] ggml_cgraph* cgraph, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* node);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_graph_print([NativeTypeName("const struct ggml_cgraph *")] ggml_cgraph* cgraph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_graph_dump_dot([NativeTypeName("const struct ggml_cgraph *")] ggml_cgraph* gb, [NativeTypeName("const struct ggml_cgraph *")] ggml_cgraph* cgraph, [NativeTypeName("const char *")] sbyte* filename);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_log_get([NativeTypeName("ggml_log_callback *")] delegate* unmanaged[Cdecl]<ggml_log_level, sbyte*, void*, void>* log_callback, void** user_data);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_log_set([NativeTypeName("ggml_log_callback")] delegate* unmanaged[Cdecl]<ggml_log_level, sbyte*, void*, void> log_callback, void* user_data);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_set_zero([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_quantize_init([NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_quantize_free();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_quantize_requires_imatrix([NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_quantize_chunk([NativeTypeName("enum ggml_type")] ggml_type type, [NativeTypeName("const float *")] float* src, void* dst, [NativeTypeName("int64_t")] long start, [NativeTypeName("int64_t")] long nrows, [NativeTypeName("int64_t")] long n_per_row, [NativeTypeName("const float *")] float* imatrix);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const struct ggml_type_traits *")]
        public static extern ggml_type_traits* ggml_get_type_traits([NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_threadpool_params")]
        public static extern ggml_threadpool_params ggml_threadpool_params_default(int n_threads);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_threadpool_params_init([NativeTypeName("struct ggml_threadpool_params *")] ggml_threadpool_params* p, int n_threads);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_threadpool_params_match([NativeTypeName("const struct ggml_threadpool_params *")] ggml_threadpool_params* p0, [NativeTypeName("const struct ggml_threadpool_params *")] ggml_threadpool_params* p1);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_backend_buft_name([NativeTypeName("ggml_backend_buffer_type_t")] ggml_backend_buffer_type* buft);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_t")]
        public static extern ggml_backend_buffer* ggml_backend_buft_alloc_buffer([NativeTypeName("ggml_backend_buffer_type_t")] ggml_backend_buffer_type* buft, [NativeTypeName("size_t")] nuint size);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_buft_get_alignment([NativeTypeName("ggml_backend_buffer_type_t")] ggml_backend_buffer_type* buft);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_buft_get_max_size([NativeTypeName("ggml_backend_buffer_type_t")] ggml_backend_buffer_type* buft);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_buft_get_alloc_size([NativeTypeName("ggml_backend_buffer_type_t")] ggml_backend_buffer_type* buft, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_buft_is_host([NativeTypeName("ggml_backend_buffer_type_t")] ggml_backend_buffer_type* buft);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_dev_t")]
        public static extern ggml_backend_device* ggml_backend_buft_get_device([NativeTypeName("ggml_backend_buffer_type_t")] ggml_backend_buffer_type* buft);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_backend_buffer_name([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_buffer_free([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void* ggml_backend_buffer_get_base([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_buffer_get_size([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_status")]
        public static extern ggml_status ggml_backend_buffer_init_tensor([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_buffer_get_alignment([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_buffer_get_max_size([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_buffer_get_alloc_size([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_buffer_clear([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer, [NativeTypeName("uint8_t")] byte value);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_buffer_is_host([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_buffer_set_usage([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer, [NativeTypeName("enum ggml_backend_buffer_usage")] ggml_backend_buffer_usage usage);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_backend_buffer_usage")]
        public static extern ggml_backend_buffer_usage ggml_backend_buffer_get_usage([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_type_t")]
        public static extern ggml_backend_buffer_type* ggml_backend_buffer_get_type([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_buffer_reset([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_tensor_copy([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* src, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* dst);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_guid_t")]
        public static extern byte** ggml_backend_guid([NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_backend_name([NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_free([NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_type_t")]
        public static extern ggml_backend_buffer_type* ggml_backend_get_default_buffer_type([NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_t")]
        public static extern ggml_backend_buffer* ggml_backend_alloc_buffer([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("size_t")] nuint size);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_get_alignment([NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_get_max_size([NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_tensor_set_async([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor, [NativeTypeName("const void *")] void* data, [NativeTypeName("size_t")] nuint offset, [NativeTypeName("size_t")] nuint size);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_tensor_get_async([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, void* data, [NativeTypeName("size_t")] nuint offset, [NativeTypeName("size_t")] nuint size);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_tensor_set_2d_async([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor, [NativeTypeName("const void *")] void* data, [NativeTypeName("size_t")] nuint offset, [NativeTypeName("size_t")] nuint size, [NativeTypeName("size_t")] nuint n_copies, [NativeTypeName("size_t")] nuint stride_tensor, [NativeTypeName("size_t")] nuint stride_data);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_tensor_get_2d_async([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, void* data, [NativeTypeName("size_t")] nuint offset, [NativeTypeName("size_t")] nuint size, [NativeTypeName("size_t")] nuint n_copies, [NativeTypeName("size_t")] nuint stride_tensor, [NativeTypeName("size_t")] nuint stride_data);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_tensor_set([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor, [NativeTypeName("const void *")] void* data, [NativeTypeName("size_t")] nuint offset, [NativeTypeName("size_t")] nuint size);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_tensor_get([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, void* data, [NativeTypeName("size_t")] nuint offset, [NativeTypeName("size_t")] nuint size);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_tensor_set_2d([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor, [NativeTypeName("const void *")] void* data, [NativeTypeName("size_t")] nuint offset, [NativeTypeName("size_t")] nuint size, [NativeTypeName("size_t")] nuint n_copies, [NativeTypeName("size_t")] nuint stride_tensor, [NativeTypeName("size_t")] nuint stride_data);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_tensor_get_2d([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, void* data, [NativeTypeName("size_t")] nuint offset, [NativeTypeName("size_t")] nuint size, [NativeTypeName("size_t")] nuint n_copies, [NativeTypeName("size_t")] nuint stride_tensor, [NativeTypeName("size_t")] nuint stride_data);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_tensor_memset([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor, [NativeTypeName("uint8_t")] byte value, [NativeTypeName("size_t")] nuint offset, [NativeTypeName("size_t")] nuint size);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_synchronize([NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_graph_plan_t")]
        public static extern void* ggml_backend_graph_plan_create([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_graph_plan_free([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("ggml_backend_graph_plan_t")] void* plan);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_status")]
        public static extern ggml_status ggml_backend_graph_plan_compute([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("ggml_backend_graph_plan_t")] void* plan);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_status")]
        public static extern ggml_status ggml_backend_graph_compute([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_status")]
        public static extern ggml_status ggml_backend_graph_compute_async([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_supports_op([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* op);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_supports_buft([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("ggml_backend_buffer_type_t")] ggml_backend_buffer_type* buft);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_offload_op([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* op);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_tensor_copy_async([NativeTypeName("ggml_backend_t")] ggml_backend* backend_src, [NativeTypeName("ggml_backend_t")] ggml_backend* backend_dst, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* src, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* dst);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_dev_t")]
        public static extern ggml_backend_device* ggml_backend_get_device([NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_event_t")]
        public static extern ggml_backend_event* ggml_backend_event_new([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_event_free([NativeTypeName("ggml_backend_event_t")] ggml_backend_event* @event);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_event_record([NativeTypeName("ggml_backend_event_t")] ggml_backend_event* @event, [NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_event_synchronize([NativeTypeName("ggml_backend_event_t")] ggml_backend_event* @event);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_event_wait([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("ggml_backend_event_t")] ggml_backend_event* @event);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_backend_dev_name([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_backend_dev_description([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_dev_memory([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device, [NativeTypeName("size_t *")] nuint* free, [NativeTypeName("size_t *")] nuint* total);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_backend_dev_type")]
        public static extern ggml_backend_dev_type ggml_backend_dev_type([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_dev_get_props([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device, [NativeTypeName("struct ggml_backend_dev_props *")] ggml_backend_dev_props* props);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_reg_t")]
        public static extern ggml_backend_reg* ggml_backend_dev_backend_reg([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_t")]
        public static extern ggml_backend* ggml_backend_dev_init([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device, [NativeTypeName("const char *")] sbyte* @params);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_type_t")]
        public static extern ggml_backend_buffer_type* ggml_backend_dev_buffer_type([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_type_t")]
        public static extern ggml_backend_buffer_type* ggml_backend_dev_host_buffer_type([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_t")]
        public static extern ggml_backend_buffer* ggml_backend_dev_buffer_from_host_ptr([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device, void* ptr, [NativeTypeName("size_t")] nuint size, [NativeTypeName("size_t")] nuint max_tensor_size);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_dev_supports_op([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* op);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_dev_supports_buft([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device, [NativeTypeName("ggml_backend_buffer_type_t")] ggml_backend_buffer_type* buft);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_dev_offload_op([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* op);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_backend_reg_name([NativeTypeName("ggml_backend_reg_t")] ggml_backend_reg* reg);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_reg_dev_count([NativeTypeName("ggml_backend_reg_t")] ggml_backend_reg* reg);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_dev_t")]
        public static extern ggml_backend_device* ggml_backend_reg_dev_get([NativeTypeName("ggml_backend_reg_t")] ggml_backend_reg* reg, [NativeTypeName("size_t")] nuint index);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void* ggml_backend_reg_get_proc_address([NativeTypeName("ggml_backend_reg_t")] ggml_backend_reg* reg, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_register([NativeTypeName("ggml_backend_reg_t")] ggml_backend_reg* reg);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_device_register([NativeTypeName("ggml_backend_dev_t")] ggml_backend_device* device);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_reg_count();

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_reg_t")]
        public static extern ggml_backend_reg* ggml_backend_reg_get([NativeTypeName("size_t")] nuint index);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_reg_t")]
        public static extern ggml_backend_reg* ggml_backend_reg_by_name([NativeTypeName("const char *")] sbyte* name);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_dev_count();

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_dev_t")]
        public static extern ggml_backend_device* ggml_backend_dev_get([NativeTypeName("size_t")] nuint index);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_dev_t")]
        public static extern ggml_backend_device* ggml_backend_dev_by_name([NativeTypeName("const char *")] sbyte* name);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_dev_t")]
        public static extern ggml_backend_device* ggml_backend_dev_by_type([NativeTypeName("enum ggml_backend_dev_type")] ggml_backend_dev_type type);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_t")]
        public static extern ggml_backend* ggml_backend_init_by_name([NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* @params);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_t")]
        public static extern ggml_backend* ggml_backend_init_by_type([NativeTypeName("enum ggml_backend_dev_type")] ggml_backend_dev_type type, [NativeTypeName("const char *")] sbyte* @params);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_t")]
        public static extern ggml_backend* ggml_backend_init_best();

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_reg_t")]
        public static extern ggml_backend_reg* ggml_backend_load([NativeTypeName("const char *")] sbyte* path);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_unload([NativeTypeName("ggml_backend_reg_t")] ggml_backend_reg* reg);

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_load_all();

        [DllImport("ggml", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_load_all_from_path([NativeTypeName("const char *")] sbyte* dir_path);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_sched_t")]
        public static extern ggml_backend_sched* ggml_backend_sched_new([NativeTypeName("ggml_backend_t *")] ggml_backend** backends, [NativeTypeName("ggml_backend_buffer_type_t *")] ggml_backend_buffer_type** bufts, int n_backends, [NativeTypeName("size_t")] nuint graph_size, [NativeTypeName("_Bool")] bool parallel, [NativeTypeName("_Bool")] bool op_offload);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_sched_free([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_sched_reserve_size([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* measure_graph, [NativeTypeName("size_t *")] nuint* sizes);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_sched_reserve([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* measure_graph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_backend_sched_get_n_backends([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_t")]
        public static extern ggml_backend* ggml_backend_sched_get_backend([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, int i);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_backend_sched_get_n_splits([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_backend_sched_get_n_copies([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_type_t")]
        public static extern ggml_backend_buffer_type* ggml_backend_sched_get_buffer_type([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, [NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ggml_backend_sched_get_buffer_size([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, [NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_sched_set_tensor_backend([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* node, [NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_t")]
        public static extern ggml_backend* ggml_backend_sched_get_tensor_backend([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* node);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_sched_split_graph([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* graph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_sched_alloc_graph([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* graph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_status")]
        public static extern ggml_status ggml_backend_sched_graph_compute([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* graph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_status")]
        public static extern ggml_status ggml_backend_sched_graph_compute_async([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* graph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_sched_synchronize([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_sched_reset([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_sched_set_eval_callback([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* sched, [NativeTypeName("ggml_backend_sched_eval_callback")] delegate* unmanaged[Cdecl]<ggml_tensor*, bool, void*, bool> callback, void* user_data);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_backend_meta_split_axis_name([NativeTypeName("enum ggml_backend_meta_split_axis")] ggml_backend_meta_split_axis split_axis);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_dev_t")]
        public static extern ggml_backend_device* ggml_backend_meta_device([NativeTypeName("ggml_backend_dev_t *")] ggml_backend_device** devs, [NativeTypeName("size_t")] nuint n_devs, [NativeTypeName("ggml_backend_meta_get_split_state_t")] delegate* unmanaged[Cdecl]<ggml_tensor*, void*, ggml_backend_meta_split_state> get_split_state, void* get_split_state_ud);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_backend_graph_copy")]
        public static extern ggml_backend_graph_copy ggml_backend_graph_copy([NativeTypeName("ggml_backend_t")] ggml_backend* backend, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* graph);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_graph_copy_free([NativeTypeName("struct ggml_backend_graph_copy")] ggml_backend_graph_copy copy);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_compare_graph_backend([NativeTypeName("ggml_backend_t")] ggml_backend* backend1, [NativeTypeName("ggml_backend_t")] ggml_backend* backend2, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* graph, [NativeTypeName("ggml_backend_eval_callback")] delegate* unmanaged[Cdecl]<int, ggml_tensor*, ggml_tensor*, void*, bool> callback, void* user_data, [NativeTypeName("const struct ggml_tensor *const *")] ggml_tensor** test_nodes, [NativeTypeName("size_t")] nuint num_test_nodes);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_status")]
        public static extern ggml_status ggml_backend_tensor_alloc([NativeTypeName("ggml_backend_buffer_t")] ggml_backend_buffer* buffer, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor, void* addr);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_status")]
        public static extern ggml_status ggml_backend_view_init([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_t")]
        public static extern ggml_backend_buffer* ggml_backend_cpu_buffer_from_ptr(void* ptr, [NativeTypeName("size_t")] nuint size);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_type_t")]
        public static extern ggml_backend_buffer_type* ggml_backend_cpu_buffer_type();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_numa_init([NativeTypeName("enum ggml_numa_strategy")] ggml_numa_strategy numa);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_is_numa();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_new_i32([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("int32_t")] int value);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_new_f32([NativeTypeName("struct ggml_context *")] ggml_context* ctx, float value);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_set_i32([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor, [NativeTypeName("int32_t")] int value);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_set_f32([NativeTypeName("struct ggml_tensor *")] ggml_tensor* tensor, float value);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int ggml_get_i32_1d([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, int i);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_set_i32_1d([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, int i, [NativeTypeName("int32_t")] int value);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int ggml_get_i32_nd([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, int i0, int i1, int i2, int i3);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_set_i32_nd([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, int i0, int i1, int i2, int i3, [NativeTypeName("int32_t")] int value);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float ggml_get_f32_1d([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, int i);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_set_f32_1d([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, int i, float value);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float ggml_get_f32_nd([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, int i0, int i1, int i2, int i3);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_set_f32_nd([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, int i0, int i1, int i2, int i3, float value);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_threadpool *")]
        public static extern ggml_threadpool* ggml_threadpool_new([NativeTypeName("struct ggml_threadpool_params *")] ggml_threadpool_params* @params);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_threadpool_free([NativeTypeName("struct ggml_threadpool *")] ggml_threadpool* threadpool);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_threadpool_pause([NativeTypeName("struct ggml_threadpool *")] ggml_threadpool* threadpool);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_threadpool_resume([NativeTypeName("struct ggml_threadpool *")] ggml_threadpool* threadpool);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_cplan")]
        public static extern ggml_cplan ggml_graph_plan([NativeTypeName("const struct ggml_cgraph *")] ggml_cgraph* cgraph, int n_threads, [NativeTypeName("struct ggml_threadpool *")] ggml_threadpool* threadpool);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_status")]
        public static extern ggml_status ggml_graph_compute([NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph, [NativeTypeName("struct ggml_cplan *")] ggml_cplan* cplan);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_status")]
        public static extern ggml_status ggml_graph_compute_with_ctx([NativeTypeName("struct ggml_context *")] ggml_context* ctx, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* cgraph, int n_threads);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_sse3();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_ssse3();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_avx();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_avx_vnni();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_avx2();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_bmi2();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_f16c();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_fma();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_avx512();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_avx512_vbmi();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_avx512_vnni();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_avx512_bf16();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_amx_int8();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_neon();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_arm_fma();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_fp16_va();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_dotprod();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_matmul_int8();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_sve();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_get_sve_cnt();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_sme();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_riscv_v();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_get_rvv_vlen();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_vsx();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_vxe();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_wasm_simd();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_cpu_has_llamafile();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const struct ggml_type_traits_cpu *")]
        public static extern ggml_type_traits_cpu* ggml_get_type_traits_cpu([NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_cpu_init();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_t")]
        public static extern ggml_backend* ggml_backend_cpu_init();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_is_cpu([NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_cpu_set_n_threads([NativeTypeName("ggml_backend_t")] ggml_backend* backend_cpu, int n_threads);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_cpu_set_threadpool([NativeTypeName("ggml_backend_t")] ggml_backend* backend_cpu, [NativeTypeName("ggml_threadpool_t")] ggml_threadpool* threadpool);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_cpu_set_abort_callback([NativeTypeName("ggml_backend_t")] ggml_backend* backend_cpu, [NativeTypeName("ggml_abort_callback")] delegate* unmanaged[Cdecl]<void*, bool> abort_callback, void* abort_callback_data);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_cpu_set_use_ref([NativeTypeName("ggml_backend_t")] ggml_backend* backend_cpu, [NativeTypeName("_Bool")] bool use_ref);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_reg_t")]
        public static extern ggml_backend_reg* ggml_backend_cpu_reg();

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_cpu_fp32_to_fp32([NativeTypeName("const float *")] float* param0, float* param1, [NativeTypeName("int64_t")] long param2);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_cpu_fp32_to_i32([NativeTypeName("const float *")] float* param0, [NativeTypeName("int32_t *")] int* param1, [NativeTypeName("int64_t")] long param2);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_cpu_fp32_to_fp16([NativeTypeName("const float *")] float* param0, [NativeTypeName("ggml_fp16_t *")] ushort* param1, [NativeTypeName("int64_t")] long param2);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_cpu_fp16_to_fp32([NativeTypeName("const ggml_fp16_t *")] ushort* param0, float* param1, [NativeTypeName("int64_t")] long param2);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_cpu_fp32_to_bf16([NativeTypeName("const float *")] float* param0, ggml_bf16_t* param1, [NativeTypeName("int64_t")] long param2);

        [DllImport("ggml-cpu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_cpu_bf16_to_fp32([NativeTypeName("const ggml_bf16_t *")] ggml_bf16_t* param0, float* param1, [NativeTypeName("int64_t")] long param2);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_opt_dataset_t")]
        public static extern ggml_opt_dataset* ggml_opt_dataset_init([NativeTypeName("enum ggml_type")] ggml_type type_data, [NativeTypeName("enum ggml_type")] ggml_type type_label, [NativeTypeName("int64_t")] long ne_datapoint, [NativeTypeName("int64_t")] long ne_label, [NativeTypeName("int64_t")] long ndata, [NativeTypeName("int64_t")] long ndata_shard);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_dataset_free([NativeTypeName("ggml_opt_dataset_t")] ggml_opt_dataset* dataset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long ggml_opt_dataset_ndata([NativeTypeName("ggml_opt_dataset_t")] ggml_opt_dataset* dataset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_opt_dataset_data([NativeTypeName("ggml_opt_dataset_t")] ggml_opt_dataset* dataset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_opt_dataset_labels([NativeTypeName("ggml_opt_dataset_t")] ggml_opt_dataset* dataset);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_dataset_shuffle([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx, [NativeTypeName("ggml_opt_dataset_t")] ggml_opt_dataset* dataset, [NativeTypeName("int64_t")] long idata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_dataset_get_batch([NativeTypeName("ggml_opt_dataset_t")] ggml_opt_dataset* dataset, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* data_batch, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* labels_batch, [NativeTypeName("int64_t")] long ibatch);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_dataset_get_batch_host([NativeTypeName("ggml_opt_dataset_t")] ggml_opt_dataset* dataset, void* data_batch, [NativeTypeName("size_t")] nuint nb_data_batch, void* labels_batch, [NativeTypeName("int64_t")] long ibatch);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_opt_optimizer_params")]
        public static extern ggml_opt_optimizer_params ggml_opt_get_default_optimizer_params(void* userdata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_opt_optimizer_params")]
        public static extern ggml_opt_optimizer_params ggml_opt_get_constant_optimizer_params(void* userdata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_opt_params")]
        public static extern ggml_opt_params ggml_opt_default_params([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* backend_sched, [NativeTypeName("enum ggml_opt_loss_type")] ggml_opt_loss_type loss_type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_opt_context_t")]
        public static extern ggml_opt_context* ggml_opt_init([NativeTypeName("struct ggml_opt_params")] ggml_opt_params @params);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_free([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_reset([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx, [NativeTypeName("_Bool")] bool optimizer);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_opt_static_graphs([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_opt_inputs([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_opt_outputs([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_opt_labels([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_opt_loss([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_opt_pred([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_opt_ncorrect([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct ggml_tensor *")]
        public static extern ggml_tensor* ggml_opt_grad_acc([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* node);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_opt_optimizer_type")]
        public static extern ggml_opt_optimizer_type ggml_opt_context_optimizer_type([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* param0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ggml_opt_optimizer_name([NativeTypeName("enum ggml_opt_optimizer_type")] ggml_opt_optimizer_type param0);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_opt_result_t")]
        public static extern ggml_opt_result* ggml_opt_result_init();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_result_free([NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_result_reset([NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_result_ndata([NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result, [NativeTypeName("int64_t *")] long* ndata);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_result_loss([NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result, double* loss, double* unc);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_result_pred([NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result, [NativeTypeName("int32_t *")] int* pred);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_result_accuracy([NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result, double* accuracy, double* unc);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_prepare_alloc([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx, [NativeTypeName("struct ggml_context *")] ggml_context* ctx_compute, [NativeTypeName("struct ggml_cgraph *")] ggml_cgraph* gf, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* inputs, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* outputs);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_alloc([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx, [NativeTypeName("_Bool")] bool backward);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_eval([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx, [NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_epoch([NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx, [NativeTypeName("ggml_opt_dataset_t")] ggml_opt_dataset* dataset, [NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result_train, [NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result_eval, [NativeTypeName("int64_t")] long idata_split, [NativeTypeName("ggml_opt_epoch_callback")] delegate* unmanaged[Cdecl]<bool, ggml_opt_context*, ggml_opt_dataset*, ggml_opt_result*, long, long, long, void> callback_train, [NativeTypeName("ggml_opt_epoch_callback")] delegate* unmanaged[Cdecl]<bool, ggml_opt_context*, ggml_opt_dataset*, ggml_opt_result*, long, long, long, void> callback_eval);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_epoch_callback_progress_bar([NativeTypeName("_Bool")] bool train, [NativeTypeName("ggml_opt_context_t")] ggml_opt_context* opt_ctx, [NativeTypeName("ggml_opt_dataset_t")] ggml_opt_dataset* dataset, [NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result, [NativeTypeName("int64_t")] long ibatch, [NativeTypeName("int64_t")] long ibatch_max, [NativeTypeName("int64_t")] long t_start_us);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_opt_fit([NativeTypeName("ggml_backend_sched_t")] ggml_backend_sched* backend_sched, [NativeTypeName("struct ggml_context *")] ggml_context* ctx_compute, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* inputs, [NativeTypeName("struct ggml_tensor *")] ggml_tensor* outputs, [NativeTypeName("ggml_opt_dataset_t")] ggml_opt_dataset* dataset, [NativeTypeName("enum ggml_opt_loss_type")] ggml_opt_loss_type loss_type, [NativeTypeName("enum ggml_opt_optimizer_type")] ggml_opt_optimizer_type optimizer, [NativeTypeName("ggml_opt_get_optimizer_params")] delegate* unmanaged[Cdecl]<void*, ggml_opt_optimizer_params> get_opt_pars, [NativeTypeName("int64_t")] long nepoch, [NativeTypeName("int64_t")] long nbatch_logical, float val_split, [NativeTypeName("_Bool")] bool silent);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct gguf_context *")]
        public static extern gguf_context* gguf_init_empty();

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct gguf_context *")]
        public static extern gguf_context* gguf_init_from_file_ptr([NativeTypeName("FILE *")] _iobuf* file, [NativeTypeName("struct gguf_init_params")] gguf_init_params @params);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct gguf_context *")]
        public static extern gguf_context* gguf_init_from_file([NativeTypeName("const char *")] sbyte* fname, [NativeTypeName("struct gguf_init_params")] gguf_init_params @params);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct gguf_context *")]
        public static extern gguf_context* gguf_init_from_buffer([NativeTypeName("const void *")] void* data, [NativeTypeName("size_t")] nuint size, [NativeTypeName("struct gguf_init_params")] gguf_init_params @params);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct gguf_context *")]
        public static extern gguf_context* gguf_init_from_callback([NativeTypeName("gguf_reader_callback_t")] delegate* unmanaged[Cdecl]<void*, void*, ulong, nuint, nuint> callback, void* userdata, [NativeTypeName("size_t")] nuint max_chunk_read, [NativeTypeName("uint64_t")] ulong max_expected_size, [NativeTypeName("struct gguf_init_params")] gguf_init_params @params);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_free([NativeTypeName("struct gguf_context *")] gguf_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* gguf_type_name([NativeTypeName("enum gguf_type")] gguf_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint gguf_get_version([NativeTypeName("const struct gguf_context *")] gguf_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint gguf_get_alignment([NativeTypeName("const struct gguf_context *")] gguf_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint gguf_get_data_offset([NativeTypeName("const struct gguf_context *")] gguf_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long gguf_get_n_kv([NativeTypeName("const struct gguf_context *")] gguf_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long gguf_find_key([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* gguf_get_key([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum gguf_type")]
        public static extern gguf_type gguf_get_kv_type([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum gguf_type")]
        public static extern gguf_type gguf_get_arr_type([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint8_t")]
        public static extern byte gguf_get_val_u8([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int8_t")]
        public static extern sbyte gguf_get_val_i8([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint16_t")]
        public static extern ushort gguf_get_val_u16([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int16_t")]
        public static extern short gguf_get_val_i16([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint gguf_get_val_u32([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int gguf_get_val_i32([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float gguf_get_val_f32([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint64_t")]
        public static extern ulong gguf_get_val_u64([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long gguf_get_val_i64([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double gguf_get_val_f64([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool gguf_get_val_bool([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* gguf_get_val_str([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const void *")]
        public static extern void* gguf_get_val_data([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint gguf_get_arr_n([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const void *")]
        public static extern void* gguf_get_arr_data([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* gguf_get_arr_str([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long key_id, [NativeTypeName("size_t")] nuint i);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long gguf_get_n_tensors([NativeTypeName("const struct gguf_context *")] gguf_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long gguf_find_tensor([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint gguf_get_tensor_offset([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long tensor_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* gguf_get_tensor_name([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long tensor_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ggml_type")]
        public static extern ggml_type gguf_get_tensor_type([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long tensor_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint gguf_get_tensor_size([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("int64_t")] long tensor_id);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long gguf_remove_key([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_u8([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("uint8_t")] byte val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_i8([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("int8_t")] sbyte val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_u16([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("uint16_t")] ushort val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_i16([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("int16_t")] short val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_u32([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("uint32_t")] uint val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_i32([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("int32_t")] int val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_f32([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, float val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_u64([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("uint64_t")] ulong val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_i64([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("int64_t")] long val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_f64([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, double val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_bool([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("_Bool")] bool val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_val_str([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("const char *")] sbyte* val);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_arr_data([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("enum gguf_type")] gguf_type type, [NativeTypeName("const void *")] void* data, [NativeTypeName("size_t")] nuint n);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_arr_str([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("size_t")] nuint n);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_kv([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const struct gguf_context *")] gguf_context* src);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_add_tensor([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_tensor_type([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("enum ggml_type")] ggml_type type);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_set_tensor_data([NativeTypeName("struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const void *")] void* data);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool gguf_write_to_file_ptr([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("FILE *")] _iobuf* file, [NativeTypeName("_Bool")] bool only_meta);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool gguf_write_to_file([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, [NativeTypeName("const char *")] sbyte* fname, [NativeTypeName("_Bool")] bool only_meta);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint gguf_get_meta_size([NativeTypeName("const struct gguf_context *")] gguf_context* ctx);

        [DllImport("ggml-base", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void gguf_get_meta_data([NativeTypeName("const struct gguf_context *")] gguf_context* ctx, void* data);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* llama_flash_attn_type_name([NativeTypeName("enum llama_flash_attn_type")] llama_flash_attn_type flash_attn_type);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_model_params")]
        public static extern llama_model_params llama_model_default_params();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_context_params")]
        public static extern llama_context_params llama_context_default_params();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler_chain_params")]
        public static extern llama_sampler_chain_params llama_sampler_chain_default_params();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_model_quantize_params")]
        public static extern llama_model_quantize_params llama_model_quantize_default_params();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_backend_init();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_backend_free();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_numa_init([NativeTypeName("enum ggml_numa_strategy")] ggml_numa_strategy numa);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_attach_threadpool([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("ggml_threadpool_t")] ggml_threadpool* threadpool, [NativeTypeName("ggml_threadpool_t")] ggml_threadpool* threadpool_batch);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_detach_threadpool([NativeTypeName("struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_model *")]
        public static extern llama_model* llama_model_init_from_user([NativeTypeName("struct gguf_context *")] gguf_context* metadata, [NativeTypeName("llama_model_set_tensor_data_t")] delegate* unmanaged[Cdecl]<ggml_tensor*, void*, void> set_tensor_data, void* set_tensor_data_ud, [NativeTypeName("struct llama_model_params")] llama_model_params @params);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_model *")]
        [Obsolete("use llama_model_load_from_file instead")]
        public static extern llama_model* llama_load_model_from_file([NativeTypeName("const char *")] sbyte* path_model, [NativeTypeName("struct llama_model_params")] llama_model_params @params);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_model *")]
        public static extern llama_model* llama_model_load_from_file([NativeTypeName("const char *")] sbyte* path_model, [NativeTypeName("struct llama_model_params")] llama_model_params @params);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_model *")]
        public static extern llama_model* llama_model_load_from_file_ptr([NativeTypeName("FILE *")] _iobuf* file, [NativeTypeName("struct llama_model_params")] llama_model_params @params);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_model *")]
        public static extern llama_model* llama_model_load_from_splits([NativeTypeName("const char **")] sbyte** paths, [NativeTypeName("size_t")] nuint n_paths, [NativeTypeName("struct llama_model_params")] llama_model_params @params);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_model_save_to_file([NativeTypeName("const struct llama_model *")] llama_model* model, [NativeTypeName("const char *")] sbyte* path_model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [Obsolete("use llama_model_free instead")]
        public static extern void llama_free_model([NativeTypeName("struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_model_free([NativeTypeName("struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_context *")]
        public static extern llama_context* llama_init_from_model([NativeTypeName("struct llama_model *")] llama_model* model, [NativeTypeName("struct llama_context_params")] llama_context_params @params);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_context *")]
        [Obsolete("use llama_init_from_model instead")]
        public static extern llama_context* llama_new_context_with_model([NativeTypeName("struct llama_model *")] llama_model* model, [NativeTypeName("struct llama_context_params")] llama_context_params @params);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_free([NativeTypeName("struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int64_t")]
        public static extern long llama_time_us();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_max_devices();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_max_parallel_sequences();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_max_tensor_buft_overrides();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_supports_mmap();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_supports_mlock();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_supports_gpu_offload();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_supports_rpc();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_n_ctx([NativeTypeName("const struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_n_ctx_seq([NativeTypeName("const struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_n_batch([NativeTypeName("const struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_n_ubatch([NativeTypeName("const struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_n_seq_max([NativeTypeName("const struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_n_rs_seq([NativeTypeName("const struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        [Obsolete("use llama_model_n_ctx_train instead")]
        public static extern int llama_n_ctx_train([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        [Obsolete("use llama_model_n_embd instead")]
        public static extern int llama_n_embd([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        [Obsolete("use llama_model_n_layer instead")]
        public static extern int llama_n_layer([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        [Obsolete("use llama_model_n_head instead")]
        public static extern int llama_n_head([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        [Obsolete("use llama_vocab_n_tokens instead")]
        public static extern int llama_n_vocab([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const struct llama_model *")]
        public static extern llama_model* llama_get_model([NativeTypeName("const struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_memory_t")]
        public static extern llama_memory_i* llama_get_memory([NativeTypeName("const struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum llama_pooling_type")]
        public static extern llama_pooling_type llama_pooling_type([NativeTypeName("const struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const struct llama_vocab *")]
        public static extern llama_vocab* llama_model_get_vocab([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum llama_rope_type")]
        public static extern llama_rope_type llama_model_rope_type([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_n_ctx_train([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_n_embd([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_n_embd_inp([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_n_embd_out([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_n_layer([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_n_head([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_n_head_kv([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_n_swa([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float llama_model_rope_freq_scale_train([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_model_n_cls_out([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* llama_model_cls_label([NativeTypeName("const struct llama_model *")] llama_model* model, [NativeTypeName("uint32_t")] uint i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum llama_vocab_type")]
        public static extern llama_vocab_type llama_vocab_type([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_vocab_n_tokens([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_meta_val_str([NativeTypeName("const struct llama_model *")] llama_model* model, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("char *")] sbyte* buf, [NativeTypeName("size_t")] nuint buf_size);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_meta_count([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* llama_model_meta_key_str([NativeTypeName("enum llama_model_meta_key")] llama_model_meta_key key);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_meta_key_by_index([NativeTypeName("const struct llama_model *")] llama_model* model, [NativeTypeName("int32_t")] int i, [NativeTypeName("char *")] sbyte* buf, [NativeTypeName("size_t")] nuint buf_size);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_meta_val_str_by_index([NativeTypeName("const struct llama_model *")] llama_model* model, [NativeTypeName("int32_t")] int i, [NativeTypeName("char *")] sbyte* buf, [NativeTypeName("size_t")] nuint buf_size);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_model_desc([NativeTypeName("const struct llama_model *")] llama_model* model, [NativeTypeName("char *")] sbyte* buf, [NativeTypeName("size_t")] nuint buf_size);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint64_t")]
        public static extern ulong llama_model_size([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* llama_model_chat_template([NativeTypeName("const struct llama_model *")] llama_model* model, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint64_t")]
        public static extern ulong llama_model_n_params([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_model_has_encoder([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_model_has_decoder([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_model_decoder_start_token([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_model_is_recurrent([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_model_is_hybrid([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_model_is_diffusion([NativeTypeName("const struct llama_model *")] llama_model* model);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_model_quantize([NativeTypeName("const char *")] sbyte* fname_inp, [NativeTypeName("const char *")] sbyte* fname_out, [NativeTypeName("const llama_model_quantize_params *")] llama_model_quantize_params* @params);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_adapter_lora *")]
        public static extern llama_adapter_lora* llama_adapter_lora_init([NativeTypeName("struct llama_model *")] llama_model* model, [NativeTypeName("const char *")] sbyte* path_lora);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_adapter_meta_val_str([NativeTypeName("const struct llama_adapter_lora *")] llama_adapter_lora* adapter, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("char *")] sbyte* buf, [NativeTypeName("size_t")] nuint buf_size);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_adapter_meta_count([NativeTypeName("const struct llama_adapter_lora *")] llama_adapter_lora* adapter);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_adapter_meta_key_by_index([NativeTypeName("const struct llama_adapter_lora *")] llama_adapter_lora* adapter, [NativeTypeName("int32_t")] int i, [NativeTypeName("char *")] sbyte* buf, [NativeTypeName("size_t")] nuint buf_size);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_adapter_meta_val_str_by_index([NativeTypeName("const struct llama_adapter_lora *")] llama_adapter_lora* adapter, [NativeTypeName("int32_t")] int i, [NativeTypeName("char *")] sbyte* buf, [NativeTypeName("size_t")] nuint buf_size);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_adapter_lora_free([NativeTypeName("struct llama_adapter_lora *")] llama_adapter_lora* adapter);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint64_t")]
        public static extern ulong llama_adapter_get_alora_n_invocation_tokens([NativeTypeName("const struct llama_adapter_lora *")] llama_adapter_lora* adapter);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const llama_token *")]
        public static extern int* llama_adapter_get_alora_invocation_tokens([NativeTypeName("const struct llama_adapter_lora *")] llama_adapter_lora* adapter);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_set_adapters_lora([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("struct llama_adapter_lora **")] llama_adapter_lora** adapters, [NativeTypeName("size_t")] nuint n_adapters, float* scales);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_set_adapter_cvec([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("const float *")] float* data, [NativeTypeName("size_t")] nuint len, [NativeTypeName("int32_t")] int n_embd, [NativeTypeName("int32_t")] int il_start, [NativeTypeName("int32_t")] int il_end);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_memory_clear([NativeTypeName("llama_memory_t")] llama_memory_i* mem, [NativeTypeName("_Bool")] bool data);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_memory_seq_rm([NativeTypeName("llama_memory_t")] llama_memory_i* mem, [NativeTypeName("llama_seq_id")] int seq_id, [NativeTypeName("llama_pos")] int p0, [NativeTypeName("llama_pos")] int p1);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_memory_seq_cp([NativeTypeName("llama_memory_t")] llama_memory_i* mem, [NativeTypeName("llama_seq_id")] int seq_id_src, [NativeTypeName("llama_seq_id")] int seq_id_dst, [NativeTypeName("llama_pos")] int p0, [NativeTypeName("llama_pos")] int p1);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_memory_seq_keep([NativeTypeName("llama_memory_t")] llama_memory_i* mem, [NativeTypeName("llama_seq_id")] int seq_id);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_memory_seq_add([NativeTypeName("llama_memory_t")] llama_memory_i* mem, [NativeTypeName("llama_seq_id")] int seq_id, [NativeTypeName("llama_pos")] int p0, [NativeTypeName("llama_pos")] int p1, [NativeTypeName("llama_pos")] int delta);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_memory_seq_div([NativeTypeName("llama_memory_t")] llama_memory_i* mem, [NativeTypeName("llama_seq_id")] int seq_id, [NativeTypeName("llama_pos")] int p0, [NativeTypeName("llama_pos")] int p1, int d);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_pos")]
        public static extern int llama_memory_seq_pos_min([NativeTypeName("llama_memory_t")] llama_memory_i* mem, [NativeTypeName("llama_seq_id")] int seq_id);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_pos")]
        public static extern int llama_memory_seq_pos_max([NativeTypeName("llama_memory_t")] llama_memory_i* mem, [NativeTypeName("llama_seq_id")] int seq_id);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_memory_can_shift([NativeTypeName("llama_memory_t")] llama_memory_i* mem);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_state_get_size([NativeTypeName("struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use llama_state_get_size instead")]
        public static extern nuint llama_get_state_size([NativeTypeName("struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_state_get_data([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("uint8_t *")] byte* dst, [NativeTypeName("size_t")] nuint size);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use llama_state_get_data instead")]
        public static extern nuint llama_copy_state_data([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("uint8_t *")] byte* dst);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_state_set_data([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("const uint8_t *")] byte* src, [NativeTypeName("size_t")] nuint size);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use llama_state_set_data instead")]
        public static extern nuint llama_set_state_data([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("const uint8_t *")] byte* src);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_state_load_file([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("const char *")] sbyte* path_session, [NativeTypeName("llama_token *")] int* tokens_out, [NativeTypeName("size_t")] nuint n_token_capacity, [NativeTypeName("size_t *")] nuint* n_token_count_out);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        [Obsolete("use llama_state_load_file instead")]
        public static extern bool llama_load_session_file([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("const char *")] sbyte* path_session, [NativeTypeName("llama_token *")] int* tokens_out, [NativeTypeName("size_t")] nuint n_token_capacity, [NativeTypeName("size_t *")] nuint* n_token_count_out);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_state_save_file([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("const char *")] sbyte* path_session, [NativeTypeName("const llama_token *")] int* tokens, [NativeTypeName("size_t")] nuint n_token_count);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        [Obsolete("use llama_state_save_file instead")]
        public static extern bool llama_save_session_file([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("const char *")] sbyte* path_session, [NativeTypeName("const llama_token *")] int* tokens, [NativeTypeName("size_t")] nuint n_token_count);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_state_seq_get_size([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("llama_seq_id")] int seq_id);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_state_seq_get_data([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("uint8_t *")] byte* dst, [NativeTypeName("size_t")] nuint size, [NativeTypeName("llama_seq_id")] int seq_id);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_state_seq_set_data([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("const uint8_t *")] byte* src, [NativeTypeName("size_t")] nuint size, [NativeTypeName("llama_seq_id")] int dest_seq_id);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_state_seq_save_file([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("const char *")] sbyte* filepath, [NativeTypeName("llama_seq_id")] int seq_id, [NativeTypeName("const llama_token *")] int* tokens, [NativeTypeName("size_t")] nuint n_token_count);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_state_seq_load_file([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("const char *")] sbyte* filepath, [NativeTypeName("llama_seq_id")] int dest_seq_id, [NativeTypeName("llama_token *")] int* tokens_out, [NativeTypeName("size_t")] nuint n_token_capacity, [NativeTypeName("size_t *")] nuint* n_token_count_out);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_state_seq_get_size_ext([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("llama_seq_id")] int seq_id, [NativeTypeName("llama_state_seq_flags")] uint flags);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_state_seq_get_data_ext([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("uint8_t *")] byte* dst, [NativeTypeName("size_t")] nuint size, [NativeTypeName("llama_seq_id")] int seq_id, [NativeTypeName("llama_state_seq_flags")] uint flags);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint llama_state_seq_set_data_ext([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("const uint8_t *")] byte* src, [NativeTypeName("size_t")] nuint size, [NativeTypeName("llama_seq_id")] int dest_seq_id, [NativeTypeName("llama_state_seq_flags")] uint flags);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_batch")]
        public static extern llama_batch llama_batch_get_one([NativeTypeName("llama_token *")] int* tokens, [NativeTypeName("int32_t")] int n_tokens);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_batch")]
        public static extern llama_batch llama_batch_init([NativeTypeName("int32_t")] int n_tokens, [NativeTypeName("int32_t")] int embd, [NativeTypeName("int32_t")] int n_seq_max);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_batch_free([NativeTypeName("struct llama_batch")] llama_batch batch);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_encode([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("struct llama_batch")] llama_batch batch);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_decode([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("struct llama_batch")] llama_batch batch);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_set_n_threads([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("int32_t")] int n_threads, [NativeTypeName("int32_t")] int n_threads_batch);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_n_threads([NativeTypeName("struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_n_threads_batch([NativeTypeName("struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_set_embeddings([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("_Bool")] bool embeddings);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_set_causal_attn([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("_Bool")] bool causal_attn);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [Obsolete("user code should do warmup runs manually [TAG_LLAMA_GRAPH_NO_WARMUP]")]
        public static extern void llama_set_warmup([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("_Bool")] bool warmup);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_set_abort_callback([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("ggml_abort_callback")] delegate* unmanaged[Cdecl]<void*, bool> abort_callback, void* abort_callback_data);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_synchronize([NativeTypeName("struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float* llama_get_logits([NativeTypeName("struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float* llama_get_logits_ith([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("int32_t")] int i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float* llama_get_embeddings([NativeTypeName("struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float* llama_get_embeddings_ith([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("int32_t")] int i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float* llama_get_embeddings_seq([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("llama_seq_id")] int seq_id);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_get_sampled_token_ith([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("int32_t")] int i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float* llama_get_sampled_probs_ith([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("int32_t")] int i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_get_sampled_probs_count_ith([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("int32_t")] int i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float* llama_get_sampled_logits_ith([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("int32_t")] int i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_get_sampled_logits_count_ith([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("int32_t")] int i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token *")]
        public static extern int* llama_get_sampled_candidates_ith([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("int32_t")] int i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_get_sampled_candidates_count_ith([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("int32_t")] int i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* llama_vocab_get_text([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("llama_token")] int token);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float llama_vocab_get_score([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("llama_token")] int token);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum llama_token_attr")]
        public static extern llama_token_attr llama_vocab_get_attr([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("llama_token")] int token);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_vocab_is_eog([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("llama_token")] int token);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_vocab_is_control([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("llama_token")] int token);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_bos([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_eos([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_eot([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_sep([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_nl([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_pad([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_mask([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_vocab_get_add_bos([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_vocab_get_add_eos([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_vocab_get_add_sep([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_fim_pre([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_fim_suf([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_fim_mid([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_fim_pad([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_fim_rep([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_vocab_fim_sep([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        [Obsolete("use llama_vocab_get_text instead")]
        public static extern sbyte* llama_token_get_text([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("llama_token")] int token);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [Obsolete("use llama_vocab_get_score instead")]
        public static extern float llama_token_get_score([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("llama_token")] int token);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum llama_token_attr")]
        [Obsolete("use llama_vocab_get_attr instead")]
        public static extern llama_token_attr llama_token_get_attr([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("llama_token")] int token);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        [Obsolete("use llama_vocab_is_eog instead")]
        public static extern bool llama_token_is_eog([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("llama_token")] int token);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        [Obsolete("use llama_vocab_is_control instead")]
        public static extern bool llama_token_is_control([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("llama_token")] int token);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_bos instead")]
        public static extern int llama_token_bos([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_eos instead")]
        public static extern int llama_token_eos([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_eot instead")]
        public static extern int llama_token_eot([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_cls instead")]
        public static extern int llama_token_cls([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_sep instead")]
        public static extern int llama_token_sep([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_nl instead")]
        public static extern int llama_token_nl([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_pad instead")]
        public static extern int llama_token_pad([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        [Obsolete("use llama_vocab_get_add_bos instead")]
        public static extern bool llama_add_bos_token([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        [Obsolete("use llama_vocab_get_add_eos instead")]
        public static extern bool llama_add_eos_token([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_fim_pre instead")]
        public static extern int llama_token_fim_pre([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_fim_suf instead")]
        public static extern int llama_token_fim_suf([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_fim_mid instead")]
        public static extern int llama_token_fim_mid([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_fim_pad instead")]
        public static extern int llama_token_fim_pad([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_fim_rep instead")]
        public static extern int llama_token_fim_rep([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_fim_sep instead")]
        public static extern int llama_token_fim_sep([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        [Obsolete("use llama_vocab_bos instead")]
        public static extern int llama_vocab_cls([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_tokenize([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("const char *")] sbyte* text, [NativeTypeName("int32_t")] int text_len, [NativeTypeName("llama_token *")] int* tokens, [NativeTypeName("int32_t")] int n_tokens_max, [NativeTypeName("_Bool")] bool add_special, [NativeTypeName("_Bool")] bool parse_special);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_token_to_piece([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("llama_token")] int token, [NativeTypeName("char *")] sbyte* buf, [NativeTypeName("int32_t")] int length, [NativeTypeName("int32_t")] int lstrip, [NativeTypeName("_Bool")] bool special);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_detokenize([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("const llama_token *")] int* tokens, [NativeTypeName("int32_t")] int n_tokens, [NativeTypeName("char *")] sbyte* text, [NativeTypeName("int32_t")] int text_len_max, [NativeTypeName("_Bool")] bool remove_special, [NativeTypeName("_Bool")] bool unparse_special);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_chat_apply_template([NativeTypeName("const char *")] sbyte* tmpl, [NativeTypeName("const struct llama_chat_message *")] llama_chat_message* chat, [NativeTypeName("size_t")] nuint n_msg, [NativeTypeName("_Bool")] bool add_ass, [NativeTypeName("char *")] sbyte* buf, [NativeTypeName("int32_t")] int length);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_chat_builtin_templates([NativeTypeName("const char **")] sbyte** output, [NativeTypeName("size_t")] nuint len);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_set_sampler([NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("llama_seq_id")] int seq_id, [NativeTypeName("struct llama_sampler *")] llama_sampler* smpl);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init([NativeTypeName("struct llama_sampler_i *")] llama_sampler_i* iface, [NativeTypeName("llama_sampler_context_t")] void* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* llama_sampler_name([NativeTypeName("const struct llama_sampler *")] llama_sampler* smpl);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_sampler_accept([NativeTypeName("struct llama_sampler *")] llama_sampler* smpl, [NativeTypeName("llama_token")] int token);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_sampler_apply([NativeTypeName("struct llama_sampler *")] llama_sampler* smpl, llama_token_data_array* cur_p);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_sampler_reset([NativeTypeName("struct llama_sampler *")] llama_sampler* smpl);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_clone([NativeTypeName("const struct llama_sampler *")] llama_sampler* smpl);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_sampler_free([NativeTypeName("struct llama_sampler *")] llama_sampler* smpl);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_chain_init([NativeTypeName("struct llama_sampler_chain_params")] llama_sampler_chain_params @params);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_sampler_chain_add([NativeTypeName("struct llama_sampler *")] llama_sampler* chain, [NativeTypeName("struct llama_sampler *")] llama_sampler* smpl);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_chain_get([NativeTypeName("struct llama_sampler *")] llama_sampler* chain, [NativeTypeName("int32_t")] int i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int llama_sampler_chain_n([NativeTypeName("const struct llama_sampler *")] llama_sampler* chain);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_chain_remove([NativeTypeName("struct llama_sampler *")] llama_sampler* chain, [NativeTypeName("int32_t")] int i);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_greedy();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_dist([NativeTypeName("uint32_t")] uint seed);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_top_k([NativeTypeName("int32_t")] int k);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_top_p(float p, [NativeTypeName("size_t")] nuint min_keep);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_min_p(float p, [NativeTypeName("size_t")] nuint min_keep);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_typical(float p, [NativeTypeName("size_t")] nuint min_keep);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_temp(float t);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_temp_ext(float t, float delta, float exponent);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_xtc(float p, float t, [NativeTypeName("size_t")] nuint min_keep, [NativeTypeName("uint32_t")] uint seed);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_top_n_sigma(float n);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_mirostat([NativeTypeName("int32_t")] int n_vocab, [NativeTypeName("uint32_t")] uint seed, float tau, float eta, [NativeTypeName("int32_t")] int m);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_mirostat_v2([NativeTypeName("uint32_t")] uint seed, float tau, float eta);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_grammar([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("const char *")] sbyte* grammar_str, [NativeTypeName("const char *")] sbyte* grammar_root);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        [Obsolete("use llama_sampler_init_grammar_lazy_patterns instead")]
        public static extern llama_sampler* llama_sampler_init_grammar_lazy([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("const char *")] sbyte* grammar_str, [NativeTypeName("const char *")] sbyte* grammar_root, [NativeTypeName("const char **")] sbyte** trigger_words, [NativeTypeName("size_t")] nuint num_trigger_words, [NativeTypeName("const llama_token *")] int* trigger_tokens, [NativeTypeName("size_t")] nuint num_trigger_tokens);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_grammar_lazy_patterns([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("const char *")] sbyte* grammar_str, [NativeTypeName("const char *")] sbyte* grammar_root, [NativeTypeName("const char **")] sbyte** trigger_patterns, [NativeTypeName("size_t")] nuint num_trigger_patterns, [NativeTypeName("const llama_token *")] int* trigger_tokens, [NativeTypeName("size_t")] nuint num_trigger_tokens);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_penalties([NativeTypeName("int32_t")] int penalty_last_n, float penalty_repeat, float penalty_freq, float penalty_present);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_dry([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab, [NativeTypeName("int32_t")] int n_ctx_train, float dry_multiplier, float dry_base, [NativeTypeName("int32_t")] int dry_allowed_length, [NativeTypeName("int32_t")] int dry_penalty_last_n, [NativeTypeName("const char **")] sbyte** seq_breakers, [NativeTypeName("size_t")] nuint num_breakers);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_adaptive_p(float target, float decay, [NativeTypeName("uint32_t")] uint seed);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_logit_bias([NativeTypeName("int32_t")] int n_vocab, [NativeTypeName("int32_t")] int n_logit_bias, [NativeTypeName("const llama_logit_bias *")] llama_logit_bias* logit_bias);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_sampler *")]
        public static extern llama_sampler* llama_sampler_init_infill([NativeTypeName("const struct llama_vocab *")] llama_vocab* vocab);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint llama_sampler_get_seed([NativeTypeName("const struct llama_sampler *")] llama_sampler* smpl);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_token")]
        public static extern int llama_sampler_sample([NativeTypeName("struct llama_sampler *")] llama_sampler* smpl, [NativeTypeName("struct llama_context *")] llama_context* ctx, [NativeTypeName("int32_t")] int idx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_split_path([NativeTypeName("char *")] sbyte* split_path, [NativeTypeName("size_t")] nuint maxlen, [NativeTypeName("const char *")] sbyte* path_prefix, [NativeTypeName("int32_t")] int split_no, [NativeTypeName("int32_t")] int split_count);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int llama_split_prefix([NativeTypeName("char *")] sbyte* split_prefix, [NativeTypeName("size_t")] nuint maxlen, [NativeTypeName("const char *")] sbyte* split_path, [NativeTypeName("int32_t")] int split_no, [NativeTypeName("int32_t")] int split_count);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* llama_print_system_info();

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_log_get([NativeTypeName("ggml_log_callback *")] delegate* unmanaged[Cdecl]<ggml_log_level, sbyte*, void*, void>* log_callback, void** user_data);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_log_set([NativeTypeName("ggml_log_callback")] delegate* unmanaged[Cdecl]<ggml_log_level, sbyte*, void*, void> log_callback, void* user_data);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_perf_context_data")]
        public static extern llama_perf_context_data llama_perf_context([NativeTypeName("const struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_perf_context_print([NativeTypeName("const struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_perf_context_reset([NativeTypeName("struct llama_context *")] llama_context* ctx);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct llama_perf_sampler_data")]
        public static extern llama_perf_sampler_data llama_perf_sampler([NativeTypeName("const struct llama_sampler *")] llama_sampler* chain);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_perf_sampler_print([NativeTypeName("const struct llama_sampler *")] llama_sampler* chain);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_perf_sampler_reset([NativeTypeName("struct llama_sampler *")] llama_sampler* chain);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool llama_opt_param_filter_all([NativeTypeName("const struct ggml_tensor *")] ggml_tensor* tensor, void* userdata);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_opt_init([NativeTypeName("struct llama_context *")] llama_context* lctx, [NativeTypeName("struct llama_model *")] llama_model* model, [NativeTypeName("struct llama_opt_params")] llama_opt_params lopt_params);

        [DllImport("llama", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void llama_opt_epoch([NativeTypeName("struct llama_context *")] llama_context* lctx, [NativeTypeName("ggml_opt_dataset_t")] ggml_opt_dataset* dataset, [NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result_train, [NativeTypeName("ggml_opt_result_t")] ggml_opt_result* result_eval, [NativeTypeName("int64_t")] long idata_split, [NativeTypeName("ggml_opt_epoch_callback")] delegate* unmanaged[Cdecl]<bool, ggml_opt_context*, ggml_opt_dataset*, ggml_opt_result*, long, long, long, void> callback_train, [NativeTypeName("ggml_opt_epoch_callback")] delegate* unmanaged[Cdecl]<bool, ggml_opt_context*, ggml_opt_dataset*, ggml_opt_result*, long, long, long, void> callback_eval);

        [DllImport("ggml-cuda", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_t")]
        public static extern ggml_backend* ggml_backend_cuda_init(int device);

        [DllImport("ggml-cuda", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_is_cuda([NativeTypeName("ggml_backend_t")] ggml_backend* backend);

        [DllImport("ggml-cuda", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_type_t")]
        public static extern ggml_backend_buffer_type* ggml_backend_cuda_buffer_type(int device);

        [DllImport("ggml-cuda", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_type_t")]
        public static extern ggml_backend_buffer_type* ggml_backend_cuda_split_buffer_type(int main_device, [NativeTypeName("const float *")] float* tensor_split);

        [DllImport("ggml-cuda", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_buffer_type_t")]
        public static extern ggml_backend_buffer_type* ggml_backend_cuda_host_buffer_type();

        [DllImport("ggml-cuda", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ggml_backend_cuda_get_device_count();

        [DllImport("ggml-cuda", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_cuda_get_device_description(int device, [NativeTypeName("char *")] sbyte* description, [NativeTypeName("size_t")] nuint description_size);

        [DllImport("ggml-cuda", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_cuda_get_device_memory(int device, [NativeTypeName("size_t *")] nuint* free, [NativeTypeName("size_t *")] nuint* total);

        [DllImport("ggml-cuda", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool ggml_backend_cuda_register_host_buffer(void* buffer, [NativeTypeName("size_t")] nuint size);

        [DllImport("ggml-cuda", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ggml_backend_cuda_unregister_host_buffer(void* buffer);

        [DllImport("ggml-cuda", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ggml_backend_reg_t")]
        public static extern ggml_backend_reg* ggml_backend_cuda_reg();

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* mtmd_default_marker();

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct mtmd_context_params")]
        public static extern mtmd_context_params mtmd_context_params_default();

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern mtmd_context* mtmd_init_from_file([NativeTypeName("const char *")] sbyte* mmproj_fname, [NativeTypeName("const struct llama_model *")] llama_model* text_model, [NativeTypeName("const struct mtmd_context_params")] mtmd_context_params ctx_params);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void mtmd_free(mtmd_context* ctx);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool mtmd_decode_use_non_causal([NativeTypeName("const mtmd_context *")] mtmd_context* ctx, [NativeTypeName("const mtmd_input_chunk *")] mtmd_input_chunk* chunk);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool mtmd_decode_use_mrope([NativeTypeName("const mtmd_context *")] mtmd_context* ctx);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool mtmd_support_vision([NativeTypeName("const mtmd_context *")] mtmd_context* ctx);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool mtmd_support_audio([NativeTypeName("const mtmd_context *")] mtmd_context* ctx);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int mtmd_get_audio_sample_rate([NativeTypeName("const mtmd_context *")] mtmd_context* ctx);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* mtmd_get_marker([NativeTypeName("const mtmd_context *")] mtmd_context* ctx);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern mtmd_bitmap* mtmd_bitmap_init([NativeTypeName("uint32_t")] uint nx, [NativeTypeName("uint32_t")] uint ny, [NativeTypeName("const unsigned char *")] byte* data);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern mtmd_bitmap* mtmd_bitmap_init_from_audio([NativeTypeName("size_t")] nuint n_samples, [NativeTypeName("const float *")] float* data);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint mtmd_bitmap_get_nx([NativeTypeName("const mtmd_bitmap *")] mtmd_bitmap* bitmap);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint mtmd_bitmap_get_ny([NativeTypeName("const mtmd_bitmap *")] mtmd_bitmap* bitmap);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const unsigned char *")]
        public static extern byte* mtmd_bitmap_get_data([NativeTypeName("const mtmd_bitmap *")] mtmd_bitmap* bitmap);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint mtmd_bitmap_get_n_bytes([NativeTypeName("const mtmd_bitmap *")] mtmd_bitmap* bitmap);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool mtmd_bitmap_is_audio([NativeTypeName("const mtmd_bitmap *")] mtmd_bitmap* bitmap);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void mtmd_bitmap_free(mtmd_bitmap* bitmap);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* mtmd_bitmap_get_id([NativeTypeName("const mtmd_bitmap *")] mtmd_bitmap* bitmap);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void mtmd_bitmap_set_id(mtmd_bitmap* bitmap, [NativeTypeName("const char *")] sbyte* id);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern mtmd_bitmap* mtmd_bitmap_init_lazy(mtmd_context* ctx, [NativeTypeName("const char *")] sbyte* id, void* user_data, [NativeTypeName("mtmd_bitmap_lazy_callback")] delegate* unmanaged[Cdecl]<nuint, void*, mtmd_bitmap**, sbyte**, int> callback);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern mtmd_input_chunks* mtmd_input_chunks_init();

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint mtmd_input_chunks_size([NativeTypeName("const mtmd_input_chunks *")] mtmd_input_chunks* chunks);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const mtmd_input_chunk *")]
        public static extern mtmd_input_chunk* mtmd_input_chunks_get([NativeTypeName("const mtmd_input_chunks *")] mtmd_input_chunks* chunks, [NativeTypeName("size_t")] nuint idx);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void mtmd_input_chunks_free(mtmd_input_chunks* chunks);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum mtmd_input_chunk_type")]
        public static extern mtmd_input_chunk_type mtmd_input_chunk_get_type([NativeTypeName("const mtmd_input_chunk *")] mtmd_input_chunk* chunk);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const llama_token *")]
        public static extern int* mtmd_input_chunk_get_tokens_text([NativeTypeName("const mtmd_input_chunk *")] mtmd_input_chunk* chunk, [NativeTypeName("size_t *")] nuint* n_tokens_output);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const mtmd_image_tokens *")]
        public static extern mtmd_image_tokens* mtmd_input_chunk_get_tokens_image([NativeTypeName("const mtmd_input_chunk *")] mtmd_input_chunk* chunk);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint mtmd_input_chunk_get_n_tokens([NativeTypeName("const mtmd_input_chunk *")] mtmd_input_chunk* chunk);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* mtmd_input_chunk_get_id([NativeTypeName("const mtmd_input_chunk *")] mtmd_input_chunk* chunk);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_pos")]
        public static extern int mtmd_input_chunk_get_n_pos([NativeTypeName("const mtmd_input_chunk *")] mtmd_input_chunk* chunk);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern mtmd_input_chunk* mtmd_input_chunk_copy([NativeTypeName("const mtmd_input_chunk *")] mtmd_input_chunk* chunk);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void mtmd_input_chunk_free(mtmd_input_chunk* chunk);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint mtmd_image_tokens_get_n_tokens([NativeTypeName("const mtmd_image_tokens *")] mtmd_image_tokens* image_tokens);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* mtmd_image_tokens_get_id([NativeTypeName("const mtmd_image_tokens *")] mtmd_image_tokens* image_tokens);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_pos")]
        public static extern int mtmd_image_tokens_get_n_pos([NativeTypeName("const mtmd_image_tokens *")] mtmd_image_tokens* image_tokens);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use mtmd_image_tokens_get_decoder_pos() instead")]
        public static extern nuint mtmd_image_tokens_get_nx([NativeTypeName("const mtmd_image_tokens *")] mtmd_image_tokens* image_tokens);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use mtmd_image_tokens_get_decoder_pos() instead")]
        public static extern nuint mtmd_image_tokens_get_ny([NativeTypeName("const mtmd_image_tokens *")] mtmd_image_tokens* image_tokens);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct mtmd_decoder_pos")]
        public static extern mtmd_decoder_pos mtmd_image_tokens_get_decoder_pos([NativeTypeName("const mtmd_image_tokens *")] mtmd_image_tokens* image_tokens, [NativeTypeName("llama_pos")] int pos_0, [NativeTypeName("size_t")] nuint i);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int mtmd_tokenize(mtmd_context* ctx, mtmd_input_chunks* output, [NativeTypeName("const mtmd_input_text *")] mtmd_input_text* text, [NativeTypeName("const mtmd_bitmap **")] mtmd_bitmap** bitmaps, [NativeTypeName("size_t")] nuint n_bitmaps);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int mtmd_encode(mtmd_context* ctx, [NativeTypeName("const mtmd_image_tokens *")] mtmd_image_tokens* image_tokens);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int mtmd_encode_chunk(mtmd_context* ctx, [NativeTypeName("const mtmd_input_chunk *")] mtmd_input_chunk* chunk);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float* mtmd_get_output_embd(mtmd_context* ctx);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void mtmd_log_set([NativeTypeName("ggml_log_callback")] delegate* unmanaged[Cdecl]<ggml_log_level, sbyte*, void*, void> log_callback, void* user_data);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct mtmd_caps")]
        public static extern mtmd_caps mtmd_get_cap_from_file([NativeTypeName("const char *")] sbyte* mmproj_fname);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern mtmd_input_chunks* mtmd_test_create_input_chunks();

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void mtmd_helper_log_set([NativeTypeName("ggml_log_callback")] delegate* unmanaged[Cdecl]<ggml_log_level, sbyte*, void*, void> log_callback, void* user_data);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern bool mtmd_helper_support_video(mtmd_context* ctx);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct mtmd_helper_bitmap_wrapper")]
        public static extern mtmd_helper_bitmap_wrapper mtmd_helper_bitmap_init_from_file(mtmd_context* ctx, [NativeTypeName("const char *")] sbyte* fname, [NativeTypeName("_Bool")] bool placeholder);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct mtmd_helper_bitmap_wrapper")]
        public static extern mtmd_helper_bitmap_wrapper mtmd_helper_bitmap_init_from_buf(mtmd_context* ctx, [NativeTypeName("const unsigned char *")] byte* buf, [NativeTypeName("size_t")] nuint len, [NativeTypeName("_Bool")] bool placeholder);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint mtmd_helper_get_n_tokens([NativeTypeName("const mtmd_input_chunks *")] mtmd_input_chunks* chunks);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("llama_pos")]
        public static extern int mtmd_helper_get_n_pos([NativeTypeName("const mtmd_input_chunks *")] mtmd_input_chunks* chunks);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void mtmd_helper_image_get_decoder_pos([NativeTypeName("const mtmd_image_tokens *")] mtmd_image_tokens* image, [NativeTypeName("llama_pos")] int pos_0, [NativeTypeName("struct mtmd_decoder_pos *")] mtmd_decoder_pos* out_pos);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int mtmd_helper_eval_chunks(mtmd_context* ctx, [NativeTypeName("struct llama_context *")] llama_context* lctx, [NativeTypeName("const mtmd_input_chunks *")] mtmd_input_chunks* chunks, [NativeTypeName("llama_pos")] int n_past, [NativeTypeName("llama_seq_id")] int seq_id, [NativeTypeName("int32_t")] int n_batch, [NativeTypeName("_Bool")] bool logits_last, [NativeTypeName("llama_pos *")] int* new_n_past);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int mtmd_helper_eval_chunk_single(mtmd_context* ctx, [NativeTypeName("struct llama_context *")] llama_context* lctx, [NativeTypeName("const mtmd_input_chunk *")] mtmd_input_chunk* chunk, [NativeTypeName("llama_pos")] int n_past, [NativeTypeName("llama_seq_id")] int seq_id, [NativeTypeName("int32_t")] int n_batch, [NativeTypeName("_Bool")] bool logits_last, [NativeTypeName("llama_pos *")] int* new_n_past);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int mtmd_helper_decode_image_chunk(mtmd_context* ctx, [NativeTypeName("struct llama_context *")] llama_context* lctx, [NativeTypeName("const mtmd_input_chunk *")] mtmd_input_chunk* chunk, float* encoded_embd, [NativeTypeName("llama_pos")] int n_past, [NativeTypeName("llama_seq_id")] int seq_id, [NativeTypeName("int32_t")] int n_batch, [NativeTypeName("llama_pos *")] int* new_n_past);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct mtmd_helper_video_init_params")]
        public static extern mtmd_helper_video_init_params mtmd_helper_video_init_params_default();

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern mtmd_helper_video* mtmd_helper_video_init([NativeTypeName("struct mtmd_context *")] mtmd_context* mctx, [NativeTypeName("const char *")] sbyte* path, [NativeTypeName("struct mtmd_helper_video_init_params")] mtmd_helper_video_init_params @params);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern mtmd_helper_video* mtmd_helper_video_init_from_buf([NativeTypeName("struct mtmd_context *")] mtmd_context* mctx, [NativeTypeName("const unsigned char *")] byte* buf, [NativeTypeName("size_t")] nuint len, [NativeTypeName("struct mtmd_helper_video_init_params")] mtmd_helper_video_init_params @params);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void mtmd_helper_video_free(mtmd_helper_video* ctx);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct mtmd_helper_video_info")]
        public static extern mtmd_helper_video_info mtmd_helper_video_get_info([NativeTypeName("const mtmd_helper_video *")] mtmd_helper_video* ctx);

        [DllImport("mtmd", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int mtmd_helper_video_read_next(mtmd_helper_video* ctx, mtmd_bitmap** out_bitmap, [NativeTypeName("char **")] sbyte** out_text);

        [NativeTypeName("#define _WIN32_WINNT 0x0A00")]
        public const int _WIN32_WINNT = 0x0A00;

        [NativeTypeName("#define GGML_FILE_MAGIC 0x67676d6c")]
        public const int GGML_FILE_MAGIC = 0x67676d6c;

        [NativeTypeName("#define GGML_FILE_VERSION 2")]
        public const int GGML_FILE_VERSION = 2;

        [NativeTypeName("#define GGML_QNT_VERSION 2")]
        public const int GGML_QNT_VERSION = 2;

        [NativeTypeName("#define GGML_QNT_VERSION_FACTOR 1000")]
        public const int GGML_QNT_VERSION_FACTOR = 1000;

        [NativeTypeName("#define GGML_MAX_DIMS 4")]
        public const int GGML_MAX_DIMS = 4;

        [NativeTypeName("#define GGML_MAX_PARAMS 2048")]
        public const int GGML_MAX_PARAMS = 2048;

        [NativeTypeName("#define GGML_MAX_SRC 10")]
        public const int GGML_MAX_SRC = 10;

        [NativeTypeName("#define GGML_MAX_N_THREADS 512")]
        public const int GGML_MAX_N_THREADS = 512;

        [NativeTypeName("#define GGML_MAX_OP_PARAMS 64")]
        public const int GGML_MAX_OP_PARAMS = 64;

        [NativeTypeName("#define GGML_MAX_NAME 64")]
        public const int GGML_MAX_NAME = 64;

        [NativeTypeName("#define GGML_DEFAULT_N_THREADS 4")]
        public const int GGML_DEFAULT_N_THREADS = 4;

        [NativeTypeName("#define GGML_DEFAULT_GRAPH_SIZE 2048")]
        public const int GGML_DEFAULT_GRAPH_SIZE = 2048;

        [NativeTypeName("#define GGML_MEM_ALIGN 16")]
        public const int GGML_MEM_ALIGN = 16;

        [NativeTypeName("#define GGML_EXIT_SUCCESS 0")]
        public const int GGML_EXIT_SUCCESS = 0;

        [NativeTypeName("#define GGML_EXIT_ABORTED 1")]
        public const int GGML_EXIT_ABORTED = 1;

        [NativeTypeName("#define GGML_ROPE_TYPE_NORMAL 0")]
        public const int GGML_ROPE_TYPE_NORMAL = 0;

        [NativeTypeName("#define GGML_ROPE_TYPE_NEOX 2")]
        public const int GGML_ROPE_TYPE_NEOX = 2;

        [NativeTypeName("#define GGML_ROPE_TYPE_MROPE 8")]
        public const int GGML_ROPE_TYPE_MROPE = 8;

        [NativeTypeName("#define GGML_ROPE_TYPE_VISION 24")]
        public const int GGML_ROPE_TYPE_VISION = 24;

        [NativeTypeName("#define GGML_ROPE_TYPE_IMROPE 40")]
        public const int GGML_ROPE_TYPE_IMROPE = 40;

        [NativeTypeName("#define GGML_MROPE_SECTIONS 4")]
        public const int GGML_MROPE_SECTIONS = 4;

        [NativeTypeName("#define GGML_N_TASKS_MAX (-1)")]
        public const int GGML_N_TASKS_MAX = (-1);

        [NativeTypeName("#define GGML_BACKEND_META_MAX_DEVICES 16")]
        public const int GGML_BACKEND_META_MAX_DEVICES = 16;

        [NativeTypeName("#define GGUF_MAGIC \"GGUF\"")]
        public static ReadOnlySpan<byte> GGUF_MAGIC => "GGUF"u8;

        [NativeTypeName("#define GGUF_VERSION 3")]
        public const int GGUF_VERSION = 3;

        [NativeTypeName("#define GGUF_KEY_GENERAL_ALIGNMENT \"general.alignment\"")]
        public static ReadOnlySpan<byte> GGUF_KEY_GENERAL_ALIGNMENT => "general.alignment"u8;

        [NativeTypeName("#define GGUF_DEFAULT_ALIGNMENT 32")]
        public const int GGUF_DEFAULT_ALIGNMENT = 32;

        [NativeTypeName("#define LLAMA_DEFAULT_SEED 0xFFFFFFFF")]
        public const uint LLAMA_DEFAULT_SEED = 0xFFFFFFFF;

        [NativeTypeName("#define LLAMA_TOKEN_NULL -1")]
        public const int LLAMA_TOKEN_NULL = -1;

        [NativeTypeName("#define LLAMA_FILE_MAGIC_GGLA 0x67676c61u")]
        public const uint LLAMA_FILE_MAGIC_GGLA = 0x67676c61U;

        [NativeTypeName("#define LLAMA_FILE_MAGIC_GGSN 0x6767736eu")]
        public const uint LLAMA_FILE_MAGIC_GGSN = 0x6767736eU;

        [NativeTypeName("#define LLAMA_FILE_MAGIC_GGSQ 0x67677371u")]
        public const uint LLAMA_FILE_MAGIC_GGSQ = 0x67677371U;

        [NativeTypeName("#define LLAMA_SESSION_MAGIC LLAMA_FILE_MAGIC_GGSN")]
        public const uint LLAMA_SESSION_MAGIC = 0x6767736eU;

        [NativeTypeName("#define LLAMA_SESSION_VERSION 9")]
        public const int LLAMA_SESSION_VERSION = 9;

        [NativeTypeName("#define LLAMA_STATE_SEQ_MAGIC LLAMA_FILE_MAGIC_GGSQ")]
        public const uint LLAMA_STATE_SEQ_MAGIC = 0x67677371U;

        [NativeTypeName("#define LLAMA_STATE_SEQ_VERSION 2")]
        public const int LLAMA_STATE_SEQ_VERSION = 2;

        [NativeTypeName("#define LLAMA_STATE_SEQ_FLAGS_NONE 0")]
        public const int LLAMA_STATE_SEQ_FLAGS_NONE = 0;

        [NativeTypeName("#define LLAMA_STATE_SEQ_FLAGS_SWA_ONLY 1")]
        public const int LLAMA_STATE_SEQ_FLAGS_SWA_ONLY = 1;

        [NativeTypeName("#define LLAMA_STATE_SEQ_FLAGS_PARTIAL_ONLY 1")]
        public const int LLAMA_STATE_SEQ_FLAGS_PARTIAL_ONLY = 1;

        [NativeTypeName("#define LLAMA_STATE_SEQ_FLAGS_ON_DEVICE 2")]
        public const int LLAMA_STATE_SEQ_FLAGS_ON_DEVICE = 2;

        [NativeTypeName("#define GGML_CUDA_NAME \"CUDA\"")]
        public static ReadOnlySpan<byte> GGML_CUDA_NAME => "CUDA"u8;

        [NativeTypeName("#define GGML_CUBLAS_NAME \"cuBLAS\"")]
        public static ReadOnlySpan<byte> GGML_CUBLAS_NAME => "cuBLAS"u8;

        [NativeTypeName("#define GGML_CUDA_MAX_DEVICES 16")]
        public const int GGML_CUDA_MAX_DEVICES = 16;
    }
}
