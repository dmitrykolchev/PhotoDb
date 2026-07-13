using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Managed.LibRaw.Native
{
    public enum LibRaw_openbayer_patterns
    {
        LIBRAW_OPENBAYER_RGGB = 0x94,
        LIBRAW_OPENBAYER_BGGR = 0x16,
        LIBRAW_OPENBAYER_GRBG = 0x61,
        LIBRAW_OPENBAYER_GBRG = 0x49,
    }

    public enum LibRaw_dngfields_marks
    {
        LIBRAW_DNGFM_FORWARDMATRIX = 1,
        LIBRAW_DNGFM_ILLUMINANT = 1 << 1,
        LIBRAW_DNGFM_COLORMATRIX = 1 << 2,
        LIBRAW_DNGFM_CALIBRATION = 1 << 3,
        LIBRAW_DNGFM_ANALOGBALANCE = 1 << 4,
        LIBRAW_DNGFM_BLACK = 1 << 5,
        LIBRAW_DNGFM_WHITE = 1 << 6,
        LIBRAW_DNGFM_OPCODE2 = 1 << 7,
        LIBRAW_DNGFM_LINTABLE = 1 << 8,
        LIBRAW_DNGFM_CROPORIGIN = 1 << 9,
        LIBRAW_DNGFM_CROPSIZE = 1 << 10,
        LIBRAW_DNGFM_PREVIEWCS = 1 << 11,
        LIBRAW_DNGFM_ASSHOTNEUTRAL = 1 << 12,
        LIBRAW_DNGFM_BASELINEEXPOSURE = 1 << 13,
        LIBRAW_DNGFM_LINEARRESPONSELIMIT = 1 << 14,
        LIBRAW_DNGFM_USERCROP = 1 << 15,
        LIBRAW_DNGFM_OPCODE1 = 1 << 16,
        LIBRAW_DNGFM_OPCODE3 = 1 << 17,
    }

    public enum LibRaw_As_Shot_WB_Applied_codes
    {
        LIBRAW_ASWB_APPLIED = 1,
        LIBRAW_ASWB_CANON = 2,
        LIBRAW_ASWB_NIKON = 4,
        LIBRAW_ASWB_NIKON_SRAW = 8,
        LIBRAW_ASWB_PENTAX = 16,
        LIBRAW_ASWB_SONY = 32,
    }

    public enum LibRaw_ExifTagTypes
    {
        LIBRAW_EXIFTAG_TYPE_UNKNOWN = 0,
        LIBRAW_EXIFTAG_TYPE_BYTE = 1,
        LIBRAW_EXIFTAG_TYPE_ASCII = 2,
        LIBRAW_EXIFTAG_TYPE_SHORT = 3,
        LIBRAW_EXIFTAG_TYPE_LONG = 4,
        LIBRAW_EXIFTAG_TYPE_RATIONAL = 5,
        LIBRAW_EXIFTAG_TYPE_SBYTE = 6,
        LIBRAW_EXIFTAG_TYPE_UNDEFINED = 7,
        LIBRAW_EXIFTAG_TYPE_SSHORT = 8,
        LIBRAW_EXIFTAG_TYPE_SLONG = 9,
        LIBRAW_EXIFTAG_TYPE_SRATIONAL = 10,
        LIBRAW_EXIFTAG_TYPE_FLOAT = 11,
        LIBRAW_EXIFTAG_TYPE_DOUBLE = 12,
        LIBRAW_EXIFTAG_TYPE_IFD = 13,
        LIBRAW_EXIFTAG_TYPE_UNICODE = 14,
        LIBRAW_EXIFTAG_TYPE_COMPLEX = 15,
        LIBRAW_EXIFTAG_TYPE_LONG8 = 16,
        LIBRAW_EXIFTAG_TYPE_SLONG8 = 17,
        LIBRAW_EXIFTAG_TYPE_IFD8 = 18,
    }

    public enum LibRaw_whitebalance_code
    {
        LIBRAW_WBI_Unknown = 0,
        LIBRAW_WBI_Daylight = 1,
        LIBRAW_WBI_Fluorescent = 2,
        LIBRAW_WBI_Tungsten = 3,
        LIBRAW_WBI_Flash = 4,
        LIBRAW_WBI_FineWeather = 9,
        LIBRAW_WBI_Cloudy = 10,
        LIBRAW_WBI_Shade = 11,
        LIBRAW_WBI_FL_D = 12,
        LIBRAW_WBI_FL_N = 13,
        LIBRAW_WBI_FL_W = 14,
        LIBRAW_WBI_FL_WW = 15,
        LIBRAW_WBI_FL_L = 16,
        LIBRAW_WBI_Ill_A = 17,
        LIBRAW_WBI_Ill_B = 18,
        LIBRAW_WBI_Ill_C = 19,
        LIBRAW_WBI_D55 = 20,
        LIBRAW_WBI_D65 = 21,
        LIBRAW_WBI_D75 = 22,
        LIBRAW_WBI_D50 = 23,
        LIBRAW_WBI_StudioTungsten = 24,
        LIBRAW_WBI_Sunset = 64,
        LIBRAW_WBI_Underwater = 65,
        LIBRAW_WBI_FluorescentHigh = 66,
        LIBRAW_WBI_HT_Mercury = 67,
        LIBRAW_WBI_AsShot = 81,
        LIBRAW_WBI_Auto = 82,
        LIBRAW_WBI_Custom = 83,
        LIBRAW_WBI_Auto1 = 85,
        LIBRAW_WBI_Auto2 = 86,
        LIBRAW_WBI_Auto3 = 87,
        LIBRAW_WBI_Auto4 = 88,
        LIBRAW_WBI_Custom1 = 90,
        LIBRAW_WBI_Custom2 = 91,
        LIBRAW_WBI_Custom3 = 92,
        LIBRAW_WBI_Custom4 = 93,
        LIBRAW_WBI_Custom5 = 94,
        LIBRAW_WBI_Custom6 = 95,
        LIBRAW_WBI_PC_Set1 = 96,
        LIBRAW_WBI_PC_Set2 = 97,
        LIBRAW_WBI_PC_Set3 = 98,
        LIBRAW_WBI_PC_Set4 = 99,
        LIBRAW_WBI_PC_Set5 = 100,
        LIBRAW_WBI_Measured = 110,
        LIBRAW_WBI_BW = 120,
        LIBRAW_WBI_Kelvin = 254,
        LIBRAW_WBI_Other = 255,
        LIBRAW_WBI_None = 0xffff,
    }

    public enum LibRaw_MultiExposure_related
    {
        LIBRAW_ME_NONE = 0,
        LIBRAW_ME_SIMPLE = 1,
        LIBRAW_ME_OVERLAY = 2,
        LIBRAW_ME_HDR = 3,
    }

    public enum LibRaw_dng_processing
    {
        LIBRAW_DNG_NONE = 0,
        LIBRAW_DNG_FLOAT = 1,
        LIBRAW_DNG_LINEAR = 2,
        LIBRAW_DNG_DEFLATE = 4,
        LIBRAW_DNG_XTRANS = 8,
        LIBRAW_DNG_OTHER = 16,
        LIBRAW_DNG_8BIT = 32,
        LIBRAW_DNG_ALL = LIBRAW_DNG_FLOAT | LIBRAW_DNG_LINEAR | LIBRAW_DNG_DEFLATE | LIBRAW_DNG_XTRANS | LIBRAW_DNG_8BIT | LIBRAW_DNG_OTHER,
        LIBRAW_DNG_DEFAULT = LIBRAW_DNG_FLOAT | LIBRAW_DNG_LINEAR | LIBRAW_DNG_DEFLATE | LIBRAW_DNG_8BIT,
    }

    public enum LibRaw_output_flags
    {
        LIBRAW_OUTPUT_FLAGS_NONE = 0,
        LIBRAW_OUTPUT_FLAGS_PPMMETA = 1,
    }

    public enum LibRaw_runtime_capabilities
    {
        LIBRAW_CAPS_RAWSPEED = 1,
        LIBRAW_CAPS_DNGSDK = 1 << 1,
        LIBRAW_CAPS_GPRSDK = 1 << 2,
        LIBRAW_CAPS_UNICODEPATHS = 1 << 3,
        LIBRAW_CAPS_X3FTOOLS = 1 << 4,
        LIBRAW_CAPS_RPI6BY9 = 1 << 5,
        LIBRAW_CAPS_ZLIB = 1 << 6,
        LIBRAW_CAPS_JPEG = 1 << 7,
        LIBRAW_CAPS_RAWSPEED3 = 1 << 8,
        LIBRAW_CAPS_RAWSPEED_BITS = 1 << 9,
    }

    public enum LibRaw_colorspace
    {
        LIBRAW_COLORSPACE_NotFound = 0,
        LIBRAW_COLORSPACE_sRGB,
        LIBRAW_COLORSPACE_AdobeRGB,
        LIBRAW_COLORSPACE_WideGamutRGB,
        LIBRAW_COLORSPACE_ProPhotoRGB,
        LIBRAW_COLORSPACE_ICC,
        LIBRAW_COLORSPACE_Uncalibrated,
        LIBRAW_COLORSPACE_CameraLinearUniWB,
        LIBRAW_COLORSPACE_CameraLinear,
        LIBRAW_COLORSPACE_CameraGammaUniWB,
        LIBRAW_COLORSPACE_CameraGamma,
        LIBRAW_COLORSPACE_MonochromeLinear,
        LIBRAW_COLORSPACE_MonochromeGamma,
        LIBRAW_COLORSPACE_Rec2020,
        LIBRAW_COLORSPACE_Unknown = 255,
    }

    public enum LibRaw_cameramaker_index
    {
        LIBRAW_CAMERAMAKER_Unknown = 0,
        LIBRAW_CAMERAMAKER_Agfa,
        LIBRAW_CAMERAMAKER_Alcatel,
        LIBRAW_CAMERAMAKER_Apple,
        LIBRAW_CAMERAMAKER_Aptina,
        LIBRAW_CAMERAMAKER_AVT,
        LIBRAW_CAMERAMAKER_Baumer,
        LIBRAW_CAMERAMAKER_Broadcom,
        LIBRAW_CAMERAMAKER_Canon,
        LIBRAW_CAMERAMAKER_Casio,
        LIBRAW_CAMERAMAKER_CINE,
        LIBRAW_CAMERAMAKER_Clauss,
        LIBRAW_CAMERAMAKER_Contax,
        LIBRAW_CAMERAMAKER_Creative,
        LIBRAW_CAMERAMAKER_DJI,
        LIBRAW_CAMERAMAKER_DXO,
        LIBRAW_CAMERAMAKER_Epson,
        LIBRAW_CAMERAMAKER_Foculus,
        LIBRAW_CAMERAMAKER_Fujifilm,
        LIBRAW_CAMERAMAKER_Generic,
        LIBRAW_CAMERAMAKER_Gione,
        LIBRAW_CAMERAMAKER_GITUP,
        LIBRAW_CAMERAMAKER_Google,
        LIBRAW_CAMERAMAKER_GoPro,
        LIBRAW_CAMERAMAKER_Hasselblad,
        LIBRAW_CAMERAMAKER_HTC,
        LIBRAW_CAMERAMAKER_I_Mobile,
        LIBRAW_CAMERAMAKER_Imacon,
        LIBRAW_CAMERAMAKER_JK_Imaging,
        LIBRAW_CAMERAMAKER_Kodak,
        LIBRAW_CAMERAMAKER_Konica,
        LIBRAW_CAMERAMAKER_Leaf,
        LIBRAW_CAMERAMAKER_Leica,
        LIBRAW_CAMERAMAKER_Lenovo,
        LIBRAW_CAMERAMAKER_LG,
        LIBRAW_CAMERAMAKER_Logitech,
        LIBRAW_CAMERAMAKER_Mamiya,
        LIBRAW_CAMERAMAKER_Matrix,
        LIBRAW_CAMERAMAKER_Meizu,
        LIBRAW_CAMERAMAKER_Micron,
        LIBRAW_CAMERAMAKER_Minolta,
        LIBRAW_CAMERAMAKER_Motorola,
        LIBRAW_CAMERAMAKER_NGM,
        LIBRAW_CAMERAMAKER_Nikon,
        LIBRAW_CAMERAMAKER_Nokia,
        LIBRAW_CAMERAMAKER_Olympus,
        LIBRAW_CAMERAMAKER_OmniVison,
        LIBRAW_CAMERAMAKER_Panasonic,
        LIBRAW_CAMERAMAKER_Parrot,
        LIBRAW_CAMERAMAKER_Pentax,
        LIBRAW_CAMERAMAKER_PhaseOne,
        LIBRAW_CAMERAMAKER_PhotoControl,
        LIBRAW_CAMERAMAKER_Photron,
        LIBRAW_CAMERAMAKER_Pixelink,
        LIBRAW_CAMERAMAKER_Polaroid,
        LIBRAW_CAMERAMAKER_RED,
        LIBRAW_CAMERAMAKER_Ricoh,
        LIBRAW_CAMERAMAKER_Rollei,
        LIBRAW_CAMERAMAKER_RoverShot,
        LIBRAW_CAMERAMAKER_Samsung,
        LIBRAW_CAMERAMAKER_Sigma,
        LIBRAW_CAMERAMAKER_Sinar,
        LIBRAW_CAMERAMAKER_SMaL,
        LIBRAW_CAMERAMAKER_Sony,
        LIBRAW_CAMERAMAKER_ST_Micro,
        LIBRAW_CAMERAMAKER_THL,
        LIBRAW_CAMERAMAKER_VLUU,
        LIBRAW_CAMERAMAKER_Xiaomi,
        LIBRAW_CAMERAMAKER_XIAOYI,
        LIBRAW_CAMERAMAKER_YI,
        LIBRAW_CAMERAMAKER_Yuneec,
        LIBRAW_CAMERAMAKER_Zeiss,
        LIBRAW_CAMERAMAKER_OnePlus,
        LIBRAW_CAMERAMAKER_ISG,
        LIBRAW_CAMERAMAKER_VIVO,
        LIBRAW_CAMERAMAKER_HMD_Global,
        LIBRAW_CAMERAMAKER_HUAWEI,
        LIBRAW_CAMERAMAKER_RaspberryPi,
        LIBRAW_CAMERAMAKER_OmDigital,
        LIBRAW_CAMERAMAKER_TheLastOne,
    }

    public enum LibRaw_camera_mounts
    {
        LIBRAW_MOUNT_Unknown = 0,
        LIBRAW_MOUNT_Alpa,
        LIBRAW_MOUNT_C,
        LIBRAW_MOUNT_Canon_EF_M,
        LIBRAW_MOUNT_Canon_EF_S,
        LIBRAW_MOUNT_Canon_EF,
        LIBRAW_MOUNT_Canon_RF,
        LIBRAW_MOUNT_Contax_N,
        LIBRAW_MOUNT_Contax645,
        LIBRAW_MOUNT_FT,
        LIBRAW_MOUNT_mFT,
        LIBRAW_MOUNT_Fuji_GF,
        LIBRAW_MOUNT_Fuji_GX,
        LIBRAW_MOUNT_Fuji_X,
        LIBRAW_MOUNT_Hasselblad_H,
        LIBRAW_MOUNT_Hasselblad_V,
        LIBRAW_MOUNT_Hasselblad_XCD,
        LIBRAW_MOUNT_Leica_M,
        LIBRAW_MOUNT_Leica_R,
        LIBRAW_MOUNT_Leica_S,
        LIBRAW_MOUNT_Leica_SL,
        LIBRAW_MOUNT_Leica_TL,
        LIBRAW_MOUNT_LPS_L,
        LIBRAW_MOUNT_Mamiya67,
        LIBRAW_MOUNT_Mamiya645,
        LIBRAW_MOUNT_Minolta_A,
        LIBRAW_MOUNT_Nikon_CX,
        LIBRAW_MOUNT_Nikon_F,
        LIBRAW_MOUNT_Nikon_Z,
        LIBRAW_MOUNT_PhaseOne_iXM_MV,
        LIBRAW_MOUNT_PhaseOne_iXM_RS,
        LIBRAW_MOUNT_PhaseOne_iXM,
        LIBRAW_MOUNT_Pentax_645,
        LIBRAW_MOUNT_Pentax_K,
        LIBRAW_MOUNT_Pentax_Q,
        LIBRAW_MOUNT_RicohModule,
        LIBRAW_MOUNT_Rollei_bayonet,
        LIBRAW_MOUNT_Samsung_NX_M,
        LIBRAW_MOUNT_Samsung_NX,
        LIBRAW_MOUNT_Sigma_X3F,
        LIBRAW_MOUNT_Sony_E,
        LIBRAW_MOUNT_LF,
        LIBRAW_MOUNT_DigitalBack,
        LIBRAW_MOUNT_FixedLens,
        LIBRAW_MOUNT_IL_UM,
        LIBRAW_MOUNT_TheLastOne,
    }

    public enum LibRaw_camera_formats
    {
        LIBRAW_FORMAT_Unknown = 0,
        LIBRAW_FORMAT_APSC,
        LIBRAW_FORMAT_FF,
        LIBRAW_FORMAT_MF,
        LIBRAW_FORMAT_APSH,
        LIBRAW_FORMAT_1INCH,
        LIBRAW_FORMAT_1div2p3INCH,
        LIBRAW_FORMAT_1div1p7INCH,
        LIBRAW_FORMAT_FT,
        LIBRAW_FORMAT_CROP645,
        LIBRAW_FORMAT_LeicaS,
        LIBRAW_FORMAT_645,
        LIBRAW_FORMAT_66,
        LIBRAW_FORMAT_69,
        LIBRAW_FORMAT_LF,
        LIBRAW_FORMAT_Leica_DMR,
        LIBRAW_FORMAT_67,
        LIBRAW_FORMAT_SigmaAPSC,
        LIBRAW_FORMAT_SigmaMerrill,
        LIBRAW_FORMAT_SigmaAPSH,
        LIBRAW_FORMAT_3648,
        LIBRAW_FORMAT_68,
        LIBRAW_FORMAT_TheLastOne,
    }

    public enum LibRawImageAspects
    {
        LIBRAW_IMAGE_ASPECT_UNKNOWN = 0,
        LIBRAW_IMAGE_ASPECT_OTHER = 1,
        LIBRAW_IMAGE_ASPECT_MINIMAL_REAL_ASPECT_VALUE = 99,
        LIBRAW_IMAGE_ASPECT_MAXIMAL_REAL_ASPECT_VALUE = 10000,
        LIBRAW_IMAGE_ASPECT_3to2 = (1000 * 3) / 2,
        LIBRAW_IMAGE_ASPECT_1to1 = 1000,
        LIBRAW_IMAGE_ASPECT_4to3 = (1000 * 4) / 3,
        LIBRAW_IMAGE_ASPECT_16to9 = (1000 * 16) / 9,
        LIBRAW_IMAGE_ASPECT_5to4 = (1000 * 5) / 4,
        LIBRAW_IMAGE_ASPECT_7to6 = (1000 * 7) / 6,
        LIBRAW_IMAGE_ASPECT_6to5 = (1000 * 6) / 5,
        LIBRAW_IMAGE_ASPECT_7to5 = (1000 * 7) / 5,
    }

    public enum LibRaw_lens_focal_types
    {
        LIBRAW_FT_UNDEFINED = 0,
        LIBRAW_FT_PRIME_LENS = 1,
        LIBRAW_FT_ZOOM_LENS = 2,
        LIBRAW_FT_ZOOM_LENS_CONSTANT_APERTURE = 3,
        LIBRAW_FT_ZOOM_LENS_VARIABLE_APERTURE = 4,
    }

    public enum LibRaw_Canon_RecordModes
    {
        LIBRAW_Canon_RecordMode_UNDEFINED = 0,
        LIBRAW_Canon_RecordMode_JPEG,
        LIBRAW_Canon_RecordMode_CRW_THM,
        LIBRAW_Canon_RecordMode_AVI_THM,
        LIBRAW_Canon_RecordMode_TIF,
        LIBRAW_Canon_RecordMode_TIF_JPEG,
        LIBRAW_Canon_RecordMode_CR2,
        LIBRAW_Canon_RecordMode_CR2_JPEG,
        LIBRAW_Canon_RecordMode_UNKNOWN,
        LIBRAW_Canon_RecordMode_MOV,
        LIBRAW_Canon_RecordMode_MP4,
        LIBRAW_Canon_RecordMode_CRM,
        LIBRAW_Canon_RecordMode_CR3,
        LIBRAW_Canon_RecordMode_CR3_JPEG,
        LIBRAW_Canon_RecordMode_HEIF,
        LIBRAW_Canon_RecordMode_CR3_HEIF,
        LIBRAW_Canon_RecordMode_TheLastOne,
    }

    public enum LibRaw_minolta_storagemethods
    {
        LIBRAW_MINOLTA_UNPACKED = 0x52,
        LIBRAW_MINOLTA_PACKED = 0x59,
    }

    public enum LibRaw_minolta_bayerpatterns
    {
        LIBRAW_MINOLTA_RGGB = 0x01,
        LIBRAW_MINOLTA_G2BRG1 = 0x04,
    }

    public enum LibRaw_sony_cameratypes
    {
        LIBRAW_SONY_DSC = 1,
        LIBRAW_SONY_DSLR = 2,
        LIBRAW_SONY_NEX = 3,
        LIBRAW_SONY_SLT = 4,
        LIBRAW_SONY_ILCE = 5,
        LIBRAW_SONY_ILCA = 6,
        LIBRAW_SONY_CameraType_UNKNOWN = 0xffff,
    }

    public enum LibRaw_Sony_0x2010_Type
    {
        LIBRAW_SONY_Tag2010None = 0,
        LIBRAW_SONY_Tag2010a,
        LIBRAW_SONY_Tag2010b,
        LIBRAW_SONY_Tag2010c,
        LIBRAW_SONY_Tag2010d,
        LIBRAW_SONY_Tag2010e,
        LIBRAW_SONY_Tag2010f,
        LIBRAW_SONY_Tag2010g,
        LIBRAW_SONY_Tag2010h,
        LIBRAW_SONY_Tag2010i,
    }

    public enum LibRaw_Sony_0x9050_Type
    {
        LIBRAW_SONY_Tag9050None = 0,
        LIBRAW_SONY_Tag9050a,
        LIBRAW_SONY_Tag9050b,
        LIBRAW_SONY_Tag9050c,
        LIBRAW_SONY_Tag9050d,
    }

    public enum LIBRAW_SONY_FOCUSMODEmodes
    {
        LIBRAW_SONY_FOCUSMODE_MF = 0,
        LIBRAW_SONY_FOCUSMODE_AF_S = 2,
        LIBRAW_SONY_FOCUSMODE_AF_C = 3,
        LIBRAW_SONY_FOCUSMODE_AF_A = 4,
        LIBRAW_SONY_FOCUSMODE_DMF = 6,
        LIBRAW_SONY_FOCUSMODE_AF_D = 7,
        LIBRAW_SONY_FOCUSMODE_AF = 101,
        LIBRAW_SONY_FOCUSMODE_PERMANENT_AF = 104,
        LIBRAW_SONY_FOCUSMODE_SEMI_MF = 105,
        LIBRAW_SONY_FOCUSMODE_UNKNOWN = -1,
    }

    public enum LibRaw_KodakSensors
    {
        LIBRAW_Kodak_UnknownSensor = 0,
        LIBRAW_Kodak_M1 = 1,
        LIBRAW_Kodak_M15 = 2,
        LIBRAW_Kodak_M16 = 3,
        LIBRAW_Kodak_M17 = 4,
        LIBRAW_Kodak_M2 = 5,
        LIBRAW_Kodak_M23 = 6,
        LIBRAW_Kodak_M24 = 7,
        LIBRAW_Kodak_M3 = 8,
        LIBRAW_Kodak_M5 = 9,
        LIBRAW_Kodak_M6 = 10,
        LIBRAW_Kodak_C14 = 11,
        LIBRAW_Kodak_X14 = 12,
        LIBRAW_Kodak_M11 = 13,
    }

    public enum LibRaw_HasselbladFormatCodes
    {
        LIBRAW_HF_Unknown = 0,
        LIBRAW_HF_3FR,
        LIBRAW_HF_FFF,
        LIBRAW_HF_Imacon,
        LIBRAW_HF_HasselbladDNG,
        LIBRAW_HF_AdobeDNG,
        LIBRAW_HF_AdobeDNG_fromPhocusDNG,
    }

    public enum LibRaw_rawspecial_t
    {
        LIBRAW_RAWSPECIAL_SONYARW2_NONE = 0,
        LIBRAW_RAWSPECIAL_SONYARW2_BASEONLY = 1,
        LIBRAW_RAWSPECIAL_SONYARW2_DELTAONLY = 1 << 1,
        LIBRAW_RAWSPECIAL_SONYARW2_DELTAZEROBASE = 1 << 2,
        LIBRAW_RAWSPECIAL_SONYARW2_DELTATOVALUE = 1 << 3,
        LIBRAW_RAWSPECIAL_SONYARW2_ALLFLAGS = LIBRAW_RAWSPECIAL_SONYARW2_BASEONLY + LIBRAW_RAWSPECIAL_SONYARW2_DELTAONLY + LIBRAW_RAWSPECIAL_SONYARW2_DELTAZEROBASE + LIBRAW_RAWSPECIAL_SONYARW2_DELTATOVALUE,
        LIBRAW_RAWSPECIAL_NODP2Q_INTERPOLATERG = 1 << 4,
        LIBRAW_RAWSPECIAL_NODP2Q_INTERPOLATEAF = 1 << 5,
        LIBRAW_RAWSPECIAL_SRAW_NO_RGB = 1 << 6,
        LIBRAW_RAWSPECIAL_SRAW_NO_INTERPOLATE = 1 << 7,
    }

    public enum LibRaw_rawspeed_bits_t
    {
        LIBRAW_RAWSPEEDV1_USE = 1,
        LIBRAW_RAWSPEEDV1_FAILONUNKNOWN = 1 << 1,
        LIBRAW_RAWSPEEDV1_IGNOREERRORS = 1 << 2,
        LIBRAW_RAWSPEEDV3_USE = 1 << 8,
        LIBRAW_RAWSPEEDV3_FAILONUNKNOWN = 1 << 9,
        LIBRAW_RAWSPEEDV3_IGNOREERRORS = 1 << 10,
    }

    public enum LibRaw_processing_options
    {
        LIBRAW_RAWOPTIONS_PENTAX_PS_ALLFRAMES = 1,
        LIBRAW_RAWOPTIONS_CONVERTFLOAT_TO_INT = 1 << 1,
        LIBRAW_RAWOPTIONS_ARQ_SKIP_CHANNEL_SWAP = 1 << 2,
        LIBRAW_RAWOPTIONS_NO_ROTATE_FOR_KODAK_THUMBNAILS = 1 << 3,
        LIBRAW_RAWOPTIONS_USE_PPM16_THUMBS = 1 << 5,
        LIBRAW_RAWOPTIONS_DONT_CHECK_DNG_ILLUMINANT = 1 << 6,
        LIBRAW_RAWOPTIONS_DNGSDK_ZEROCOPY = 1 << 7,
        LIBRAW_RAWOPTIONS_ZEROFILTERS_FOR_MONOCHROMETIFFS = 1 << 8,
        LIBRAW_RAWOPTIONS_DNG_ADD_ENHANCED = 1 << 9,
        LIBRAW_RAWOPTIONS_DNG_ADD_PREVIEWS = 1 << 10,
        LIBRAW_RAWOPTIONS_DNG_PREFER_LARGEST_IMAGE = 1 << 11,
        LIBRAW_RAWOPTIONS_DNG_STAGE2 = 1 << 12,
        LIBRAW_RAWOPTIONS_DNG_STAGE3 = 1 << 13,
        LIBRAW_RAWOPTIONS_DNG_ALLOWSIZECHANGE = 1 << 14,
        LIBRAW_RAWOPTIONS_DNG_DISABLEWBADJUST = 1 << 15,
        LIBRAW_RAWOPTIONS_PROVIDE_NONSTANDARD_WB = 1 << 16,
        LIBRAW_RAWOPTIONS_CAMERAWB_FALLBACK_TO_DAYLIGHT = 1 << 17,
        LIBRAW_RAWOPTIONS_CHECK_THUMBNAILS_KNOWN_VENDORS = 1 << 18,
        LIBRAW_RAWOPTIONS_CHECK_THUMBNAILS_ALL_VENDORS = 1 << 19,
        LIBRAW_RAWOPTIONS_DNG_STAGE2_IFPRESENT = 1 << 20,
        LIBRAW_RAWOPTIONS_DNG_STAGE3_IFPRESENT = 1 << 21,
        LIBRAW_RAWOPTIONS_DNG_ADD_MASKS = 1 << 22,
        LIBRAW_RAWOPTIONS_CANON_IGNORE_MAKERNOTES_ROTATION = 1 << 23,
        LIBRAW_RAWOPTIONS_ALLOW_JPEGXL_PREVIEWS = 1 << 24,
        LIBRAW_RAWOPTIONS_CANON_CHECK_CAMERA_AUTO_ROTATION_MODE = 1 << 26,
        LIBRAW_RAWOPTIONS_DNG_STAGE23_IFPRESENT_JPGJXL = 1 << 27,
    }

    public enum LibRaw_decoder_flags
    {
        LIBRAW_DECODER_HASCURVE = 1 << 4,
        LIBRAW_DECODER_SONYARW2 = 1 << 5,
        LIBRAW_DECODER_TRYRAWSPEED = 1 << 6,
        LIBRAW_DECODER_OWNALLOC = 1 << 7,
        LIBRAW_DECODER_FIXEDMAXC = 1 << 8,
        LIBRAW_DECODER_ADOBECOPYPIXEL = 1 << 9,
        LIBRAW_DECODER_LEGACY_WITH_MARGINS = 1 << 10,
        LIBRAW_DECODER_3CHANNEL = 1 << 11,
        LIBRAW_DECODER_SINAR4SHOT = 1 << 11,
        LIBRAW_DECODER_FLATDATA = 1 << 12,
        LIBRAW_DECODER_FLAT_BG2_SWAPPED = 1 << 13,
        LIBRAW_DECODER_UNSUPPORTED_FORMAT = 1 << 14,
        LIBRAW_DECODER_NOTSET = 1 << 15,
        LIBRAW_DECODER_TRYRAWSPEED3 = 1 << 16,
    }

    public enum LibRaw_constructor_flags
    {
        LIBRAW_OPTIONS_NONE = 0,
        LIBRAW_OPTIONS_NO_DATAERR_CALLBACK = 1 << 1,
        LIBRAW_OPIONS_NO_DATAERR_CALLBACK = LIBRAW_OPTIONS_NO_DATAERR_CALLBACK,
    }

    public enum LibRaw_warnings
    {
        LIBRAW_WARN_NONE = 0,
        LIBRAW_WARN_BAD_CAMERA_WB = 1 << 2,
        LIBRAW_WARN_NO_METADATA = 1 << 3,
        LIBRAW_WARN_NO_JPEGLIB = 1 << 4,
        LIBRAW_WARN_NO_EMBEDDED_PROFILE = 1 << 5,
        LIBRAW_WARN_NO_INPUT_PROFILE = 1 << 6,
        LIBRAW_WARN_BAD_OUTPUT_PROFILE = 1 << 7,
        LIBRAW_WARN_NO_BADPIXELMAP = 1 << 8,
        LIBRAW_WARN_BAD_DARKFRAME_FILE = 1 << 9,
        LIBRAW_WARN_BAD_DARKFRAME_DIM = 1 << 10,
        LIBRAW_WARN_RAWSPEED_PROBLEM = 1 << 12,
        LIBRAW_WARN_RAWSPEED_UNSUPPORTED = 1 << 13,
        LIBRAW_WARN_RAWSPEED_PROCESSED = 1 << 14,
        LIBRAW_WARN_FALLBACK_TO_AHD = 1 << 15,
        LIBRAW_WARN_PARSEFUJI_PROCESSED = 1 << 16,
        LIBRAW_WARN_DNGSDK_PROCESSED = 1 << 17,
        LIBRAW_WARN_DNG_IMAGES_REORDERED = 1 << 18,
        LIBRAW_WARN_DNG_STAGE2_APPLIED = 1 << 19,
        LIBRAW_WARN_DNG_STAGE3_APPLIED = 1 << 20,
        LIBRAW_WARN_RAWSPEED3_PROBLEM = 1 << 21,
        LIBRAW_WARN_RAWSPEED3_UNSUPPORTED = 1 << 22,
        LIBRAW_WARN_RAWSPEED3_PROCESSED = 1 << 23,
        LIBRAW_WARN_RAWSPEED3_NOTLISTED = 1 << 24,
        LIBRAW_WARN_VENDOR_CROP_SUGGESTED = 1 << 25,
        LIBRAW_WARN_DNG_NOT_PROCESSED = 1 << 26,
        LIBRAW_WARN_DNG_NOT_PARSED = 1 << 27,
    }

    public enum LibRaw_exceptions
    {
        LIBRAW_EXCEPTION_NONE = 0,
        LIBRAW_EXCEPTION_ALLOC = 1,
        LIBRAW_EXCEPTION_DECODE_RAW = 2,
        LIBRAW_EXCEPTION_DECODE_JPEG = 3,
        LIBRAW_EXCEPTION_IO_EOF = 4,
        LIBRAW_EXCEPTION_IO_CORRUPT = 5,
        LIBRAW_EXCEPTION_CANCELLED_BY_CALLBACK = 6,
        LIBRAW_EXCEPTION_BAD_CROP = 7,
        LIBRAW_EXCEPTION_IO_BADFILE = 8,
        LIBRAW_EXCEPTION_DECODE_JPEG2000 = 9,
        LIBRAW_EXCEPTION_TOOBIG = 10,
        LIBRAW_EXCEPTION_MEMPOOL = 11,
        LIBRAW_EXCEPTION_UNSUPPORTED_FORMAT = 12,
    }

    public enum LibRaw_progress
    {
        LIBRAW_PROGRESS_START = 0,
        LIBRAW_PROGRESS_OPEN = 1,
        LIBRAW_PROGRESS_IDENTIFY = 1 << 1,
        LIBRAW_PROGRESS_SIZE_ADJUST = 1 << 2,
        LIBRAW_PROGRESS_LOAD_RAW = 1 << 3,
        LIBRAW_PROGRESS_RAW2_IMAGE = 1 << 4,
        LIBRAW_PROGRESS_REMOVE_ZEROES = 1 << 5,
        LIBRAW_PROGRESS_BAD_PIXELS = 1 << 6,
        LIBRAW_PROGRESS_DARK_FRAME = 1 << 7,
        LIBRAW_PROGRESS_FOVEON_INTERPOLATE = 1 << 8,
        LIBRAW_PROGRESS_SCALE_COLORS = 1 << 9,
        LIBRAW_PROGRESS_PRE_INTERPOLATE = 1 << 10,
        LIBRAW_PROGRESS_INTERPOLATE = 1 << 11,
        LIBRAW_PROGRESS_MIX_GREEN = 1 << 12,
        LIBRAW_PROGRESS_MEDIAN_FILTER = 1 << 13,
        LIBRAW_PROGRESS_HIGHLIGHTS = 1 << 14,
        LIBRAW_PROGRESS_FUJI_ROTATE = 1 << 15,
        LIBRAW_PROGRESS_FLIP = 1 << 16,
        LIBRAW_PROGRESS_APPLY_PROFILE = 1 << 17,
        LIBRAW_PROGRESS_CONVERT_RGB = 1 << 18,
        LIBRAW_PROGRESS_STRETCH = 1 << 19,
        LIBRAW_PROGRESS_STAGE20 = 1 << 20,
        LIBRAW_PROGRESS_STAGE21 = 1 << 21,
        LIBRAW_PROGRESS_STAGE22 = 1 << 22,
        LIBRAW_PROGRESS_STAGE23 = 1 << 23,
        LIBRAW_PROGRESS_STAGE24 = 1 << 24,
        LIBRAW_PROGRESS_STAGE25 = 1 << 25,
        LIBRAW_PROGRESS_STAGE26 = 1 << 26,
        LIBRAW_PROGRESS_STAGE27 = 1 << 27,
        LIBRAW_PROGRESS_THUMB_LOAD = 1 << 28,
        LIBRAW_PROGRESS_TRESERVED1 = 1 << 29,
        LIBRAW_PROGRESS_TRESERVED2 = 1 << 30,
    }

    public enum LibRaw_errors
    {
        LIBRAW_SUCCESS = 0,
        LIBRAW_UNSPECIFIED_ERROR = -1,
        LIBRAW_FILE_UNSUPPORTED = -2,
        LIBRAW_REQUEST_FOR_NONEXISTENT_IMAGE = -3,
        LIBRAW_OUT_OF_ORDER_CALL = -4,
        LIBRAW_NO_THUMBNAIL = -5,
        LIBRAW_UNSUPPORTED_THUMBNAIL = -6,
        LIBRAW_INPUT_CLOSED = -7,
        LIBRAW_NOT_IMPLEMENTED = -8,
        LIBRAW_REQUEST_FOR_NONEXISTENT_THUMBNAIL = -9,
        LIBRAW_UNSUFFICIENT_MEMORY = -100007,
        LIBRAW_DATA_ERROR = -100008,
        LIBRAW_IO_ERROR = -100009,
        LIBRAW_CANCELLED_BY_CALLBACK = -100010,
        LIBRAW_BAD_CROP = -100011,
        LIBRAW_TOO_BIG = -100012,
        LIBRAW_MEMPOOL_OVERFLOW = -100013,
    }

    public enum LibRaw_internal_thumbnail_formats
    {
        LIBRAW_INTERNAL_THUMBNAIL_UNKNOWN = 0,
        LIBRAW_INTERNAL_THUMBNAIL_KODAK_THUMB = 1,
        LIBRAW_INTERNAL_THUMBNAIL_KODAK_YCBCR = 2,
        LIBRAW_INTERNAL_THUMBNAIL_KODAK_RGB = 3,
        LIBRAW_INTERNAL_THUMBNAIL_JPEG = 4,
        LIBRAW_INTERNAL_THUMBNAIL_LAYER,
        LIBRAW_INTERNAL_THUMBNAIL_ROLLEI,
        LIBRAW_INTERNAL_THUMBNAIL_PPM,
        LIBRAW_INTERNAL_THUMBNAIL_PPM16,
        LIBRAW_INTERNAL_THUMBNAIL_X3F,
        LIBRAW_INTERNAL_THUMBNAIL_DNG_YCBCR,
        LIBRAW_INTERNAL_THUMBNAIL_JPEGXL,
    }

    public enum LibRaw_thumbnail_formats
    {
        LIBRAW_THUMBNAIL_UNKNOWN = 0,
        LIBRAW_THUMBNAIL_JPEG = 1,
        LIBRAW_THUMBNAIL_BITMAP = 2,
        LIBRAW_THUMBNAIL_BITMAP16 = 3,
        LIBRAW_THUMBNAIL_LAYER = 4,
        LIBRAW_THUMBNAIL_ROLLEI = 5,
        LIBRAW_THUMBNAIL_H265 = 6,
        LIBRAW_THUMBNAIL_JPEGXL = 7,
    }

    public enum LibRaw_image_formats
    {
        LIBRAW_IMAGE_JPEG = 1,
        LIBRAW_IMAGE_BITMAP = 2,
        LIBRAW_IMAGE_JPEGXL = 3,
        LIBRAW_IMAGE_H265 = 4,
    }

    public unsafe partial struct libraw_decoder_info_t
    {
        [NativeTypeName("const char *")]
        public sbyte* decoder_name;

        [NativeTypeName("unsigned int")]
        public uint decoder_flags;
    }

    public partial struct libraw_internal_output_params_t
    {
        [NativeTypeName("unsigned int")]
        public uint mix_green;

        [NativeTypeName("unsigned int")]
        public uint raw_color;

        [NativeTypeName("unsigned int")]
        public uint zero_is_bad;

        public ushort shrink;

        public ushort fuji_width;
    }

    public unsafe partial struct libraw_callbacks_t
    {
        [NativeTypeName("data_callback")]
        public delegate* unmanaged[Cdecl]<void*, sbyte*, long, void> data_cb;

        public void* datacb_data;

        [NativeTypeName("progress_callback")]
        public delegate* unmanaged[Cdecl]<void*, LibRaw_progress, int, int, int> progress_cb;

        public void* progresscb_data;

        [NativeTypeName("exif_parser_callback")]
        public delegate* unmanaged[Cdecl]<void*, int, int, int, uint, void*, long, void> exif_cb;

        [NativeTypeName("exif_parser_callback")]
        public delegate* unmanaged[Cdecl]<void*, int, int, int, uint, void*, long, void> makernotes_cb;

        public void* exifparser_data;

        public void* makernotesparser_data;

        [NativeTypeName("pre_identify_callback")]
        public delegate* unmanaged[Cdecl]<void*, int> pre_identify_cb;

        [NativeTypeName("post_identify_callback")]
        public delegate* unmanaged[Cdecl]<void*, void> post_identify_cb;

        [NativeTypeName("process_step_callback")]
        public delegate* unmanaged[Cdecl]<void*, void> pre_subtractblack_cb;

        [NativeTypeName("process_step_callback")]
        public delegate* unmanaged[Cdecl]<void*, void> pre_scalecolors_cb;

        [NativeTypeName("process_step_callback")]
        public delegate* unmanaged[Cdecl]<void*, void> pre_preinterpolate_cb;

        [NativeTypeName("process_step_callback")]
        public delegate* unmanaged[Cdecl]<void*, void> pre_interpolate_cb;

        [NativeTypeName("process_step_callback")]
        public delegate* unmanaged[Cdecl]<void*, void> interpolate_bayer_cb;

        [NativeTypeName("process_step_callback")]
        public delegate* unmanaged[Cdecl]<void*, void> interpolate_xtrans_cb;

        [NativeTypeName("process_step_callback")]
        public delegate* unmanaged[Cdecl]<void*, void> post_interpolate_cb;

        [NativeTypeName("process_step_callback")]
        public delegate* unmanaged[Cdecl]<void*, void> pre_converttorgb_cb;

        [NativeTypeName("process_step_callback")]
        public delegate* unmanaged[Cdecl]<void*, void> post_converttorgb_cb;
    }

    public partial struct libraw_processed_image_t
    {
        [NativeTypeName("enum LibRaw_image_formats")]
        public LibRaw_image_formats type;

        public ushort height;

        public ushort width;

        public ushort colors;

        public ushort bits;

        [NativeTypeName("unsigned int")]
        public uint data_size;

        [NativeTypeName("unsigned char[1]")]
        public _data_e__FixedBuffer data;

        public partial struct _data_e__FixedBuffer
        {
            public byte e0;

            [UnscopedRef]
            public ref byte this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return ref Unsafe.Add(ref e0, index);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [UnscopedRef]
            public Span<byte> AsSpan(int length) => MemoryMarshal.CreateSpan(ref e0, length);
        }
    }

    public unsafe partial struct libraw_iparams_t
    {
        [NativeTypeName("char[4]")]
        public _guard_e__FixedBuffer guard;

        [NativeTypeName("char[64]")]
        public _make_e__FixedBuffer make;

        [NativeTypeName("char[64]")]
        public _model_e__FixedBuffer model;

        [NativeTypeName("char[64]")]
        public _software_e__FixedBuffer software;

        [NativeTypeName("char[64]")]
        public _normalized_make_e__FixedBuffer normalized_make;

        [NativeTypeName("char[64]")]
        public _normalized_model_e__FixedBuffer normalized_model;

        [NativeTypeName("unsigned int")]
        public uint maker_index;

        [NativeTypeName("unsigned int")]
        public uint raw_count;

        [NativeTypeName("unsigned int")]
        public uint dng_version;

        [NativeTypeName("unsigned int")]
        public uint is_foveon;

        public int colors;

        [NativeTypeName("unsigned int")]
        public uint filters;

        [NativeTypeName("char[6][6]")]
        public _xtrans_e__FixedBuffer xtrans;

        [NativeTypeName("char[6][6]")]
        public _xtrans_abs_e__FixedBuffer xtrans_abs;

        [NativeTypeName("char[5]")]
        public _cdesc_e__FixedBuffer cdesc;

        [NativeTypeName("unsigned int")]
        public uint xmplen;

        [NativeTypeName("char *")]
        public sbyte* xmpdata;

        [InlineArray(4)]
        public partial struct _guard_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _make_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _model_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _software_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _normalized_make_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _normalized_model_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(6 * 6)]
        public partial struct _xtrans_e__FixedBuffer
        {
            public sbyte e0_0;
        }

        [InlineArray(6 * 6)]
        public partial struct _xtrans_abs_e__FixedBuffer
        {
            public sbyte e0_0;
        }

        [InlineArray(5)]
        public partial struct _cdesc_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public partial struct libraw_raw_inset_crop_t
    {
        public ushort cleft;

        public ushort ctop;

        public ushort cwidth;

        public ushort cheight;
    }

    public partial struct libraw_image_sizes_t
    {
        public ushort raw_height;

        public ushort raw_width;

        public ushort height;

        public ushort width;

        public ushort top_margin;

        public ushort left_margin;

        public ushort iheight;

        public ushort iwidth;

        [NativeTypeName("unsigned int")]
        public uint raw_pitch;

        public double pixel_aspect;

        public int flip;

        [NativeTypeName("int[8][4]")]
        public _mask_e__FixedBuffer mask;

        public ushort raw_aspect;

        [NativeTypeName("libraw_raw_inset_crop_t[2]")]
        public _raw_inset_crops_e__FixedBuffer raw_inset_crops;

        [InlineArray(8 * 4)]
        public partial struct _mask_e__FixedBuffer
        {
            public int e0_0;
        }

        [InlineArray(2)]
        public partial struct _raw_inset_crops_e__FixedBuffer
        {
            public libraw_raw_inset_crop_t e0;
        }
    }

    public partial struct libraw_area_t
    {
        public short t;

        public short l;

        public short b;

        public short r;
    }

    public partial struct ph1_t
    {
        public int format;

        public int key_off;

        public int tag_21a;

        public int t_black;

        public int split_col;

        public int black_col;

        public int split_row;

        public int black_row;

        public float tag_210;
    }

    public partial struct libraw_dng_color_t
    {
        [NativeTypeName("unsigned int")]
        public uint parsedfields;

        public ushort illuminant;

        [NativeTypeName("float[4][4]")]
        public _calibration_e__FixedBuffer calibration;

        [NativeTypeName("float[4][3]")]
        public _colormatrix_e__FixedBuffer colormatrix;

        [NativeTypeName("float[3][4]")]
        public _forwardmatrix_e__FixedBuffer forwardmatrix;

        [InlineArray(4 * 4)]
        public partial struct _calibration_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(4 * 3)]
        public partial struct _colormatrix_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(3 * 4)]
        public partial struct _forwardmatrix_e__FixedBuffer
        {
            public float e0_0;
        }
    }

    public unsafe partial struct libraw_dng_rawopcode_t
    {
        [NativeTypeName("unsigned int")]
        public uint len;

        public void* data;
    }

    public partial struct libraw_dng_levels_t
    {
        [NativeTypeName("unsigned int")]
        public uint parsedfields;

        [NativeTypeName("unsigned int[4104]")]
        public _dng_cblack_e__FixedBuffer dng_cblack;

        [NativeTypeName("unsigned int")]
        public uint dng_black;

        [NativeTypeName("float[4104]")]
        public _dng_fcblack_e__FixedBuffer dng_fcblack;

        public float dng_fblack;

        [NativeTypeName("unsigned int[4]")]
        public _dng_whitelevel_e__FixedBuffer dng_whitelevel;

        [NativeTypeName("ushort[4]")]
        public _default_crop_e__FixedBuffer default_crop;

        [NativeTypeName("float[4]")]
        public _user_crop_e__FixedBuffer user_crop;

        [NativeTypeName("unsigned int")]
        public uint preview_colorspace;

        [NativeTypeName("float[4]")]
        public _analogbalance_e__FixedBuffer analogbalance;

        [NativeTypeName("float[4]")]
        public _asshotneutral_e__FixedBuffer asshotneutral;

        public float baseline_exposure;

        public float LinearResponseLimit;

        [NativeTypeName("libraw_dng_rawopcode_t[3]")]
        public _rawopcodes_e__FixedBuffer rawopcodes;

        [InlineArray(4104)]
        public partial struct _dng_cblack_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(4104)]
        public partial struct _dng_fcblack_e__FixedBuffer
        {
            public float e0;
        }

        [InlineArray(4)]
        public partial struct _dng_whitelevel_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(4)]
        public partial struct _default_crop_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(4)]
        public partial struct _user_crop_e__FixedBuffer
        {
            public float e0;
        }

        [InlineArray(4)]
        public partial struct _analogbalance_e__FixedBuffer
        {
            public float e0;
        }

        [InlineArray(4)]
        public partial struct _asshotneutral_e__FixedBuffer
        {
            public float e0;
        }

        [InlineArray(3)]
        public partial struct _rawopcodes_e__FixedBuffer
        {
            public libraw_dng_rawopcode_t e0;
        }
    }

    public partial struct libraw_P1_color_t
    {
        [NativeTypeName("float[9]")]
        public _romm_cam_e__FixedBuffer romm_cam;

        [InlineArray(9)]
        public partial struct _romm_cam_e__FixedBuffer
        {
            public float e0;
        }
    }

    public partial struct libraw_canon_makernotes_t
    {
        public int ColorDataVer;

        public int ColorDataSubVer;

        public int SpecularWhiteLevel;

        public int NormalWhiteLevel;

        [NativeTypeName("int[4]")]
        public _ChannelBlackLevel_e__FixedBuffer ChannelBlackLevel;

        public int AverageBlackLevel;

        [NativeTypeName("unsigned int[4]")]
        public _multishot_e__FixedBuffer multishot;

        public short MeteringMode;

        public short SpotMeteringMode;

        [NativeTypeName("uchar")]
        public byte FlashMeteringMode;

        public short FlashExposureLock;

        public short ExposureMode;

        public short AESetting;

        public short ImageStabilization;

        public short FlashMode;

        public short FlashActivity;

        public short FlashBits;

        public short ManualFlashOutput;

        public short FlashOutput;

        public short FlashGuideNumber;

        public short ContinuousDrive;

        public short SensorWidth;

        public short SensorHeight;

        public int AFMicroAdjMode;

        public float AFMicroAdjValue;

        public short MakernotesFlip;

        public short AutoRotateMode;

        public short RecordMode;

        public short SRAWQuality;

        [NativeTypeName("unsigned int")]
        public uint wbi;

        public short RF_lensID;

        public int AutoLightingOptimizer;

        public int HighlightTonePriority;

        public short Quality;

        public int CanonLog;

        public libraw_area_t DefaultCropAbsolute;

        public libraw_area_t RecommendedImageArea;

        public libraw_area_t LeftOpticalBlack;

        public libraw_area_t UpperOpticalBlack;

        public libraw_area_t ActiveArea;

        [NativeTypeName("short[2]")]
        public _ISOgain_e__FixedBuffer ISOgain;

        [InlineArray(4)]
        public partial struct _ChannelBlackLevel_e__FixedBuffer
        {
            public int e0;
        }

        [InlineArray(4)]
        public partial struct _multishot_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(2)]
        public partial struct _ISOgain_e__FixedBuffer
        {
            public short e0;
        }
    }

    public partial struct libraw_hasselblad_makernotes_t
    {
        public int BaseISO;

        public double Gain;

        [NativeTypeName("char[8]")]
        public _Sensor_e__FixedBuffer Sensor;

        [NativeTypeName("char[64]")]
        public _SensorUnit_e__FixedBuffer SensorUnit;

        [NativeTypeName("char[64]")]
        public _HostBody_e__FixedBuffer HostBody;

        public int SensorCode;

        public int SensorSubCode;

        public int CoatingCode;

        public int uncropped;

        [NativeTypeName("char[32]")]
        public _CaptureSequenceInitiator_e__FixedBuffer CaptureSequenceInitiator;

        [NativeTypeName("char[64]")]
        public _SensorUnitConnector_e__FixedBuffer SensorUnitConnector;

        public int format;

        [NativeTypeName("int[2]")]
        public _nIFD_CM_e__FixedBuffer nIFD_CM;

        [NativeTypeName("int[2]")]
        public _RecommendedCrop_e__FixedBuffer RecommendedCrop;

        [NativeTypeName("double[4][3]")]
        public _mnColorMatrix_e__FixedBuffer mnColorMatrix;

        [InlineArray(8)]
        public partial struct _Sensor_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _SensorUnit_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _HostBody_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(32)]
        public partial struct _CaptureSequenceInitiator_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _SensorUnitConnector_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(2)]
        public partial struct _nIFD_CM_e__FixedBuffer
        {
            public int e0;
        }

        [InlineArray(2)]
        public partial struct _RecommendedCrop_e__FixedBuffer
        {
            public int e0;
        }

        [InlineArray(4 * 3)]
        public partial struct _mnColorMatrix_e__FixedBuffer
        {
            public double e0_0;
        }
    }

    public partial struct libraw_fuji_info_t
    {
        public float ExpoMidPointShift;

        public ushort DynamicRange;

        public ushort FilmMode;

        public ushort DynamicRangeSetting;

        public ushort DevelopmentDynamicRange;

        public ushort AutoDynamicRange;

        public ushort DRangePriority;

        public ushort DRangePriorityAuto;

        public ushort DRangePriorityFixed;

        [NativeTypeName("char[33]")]
        public _FujiModel_e__FixedBuffer FujiModel;

        [NativeTypeName("char[33]")]
        public _FujiModel2_e__FixedBuffer FujiModel2;

        public float BrightnessCompensation;

        public ushort FocusMode;

        public ushort AFMode;

        [NativeTypeName("ushort[2]")]
        public _FocusPixel_e__FixedBuffer FocusPixel;

        public ushort PrioritySettings;

        [NativeTypeName("unsigned int")]
        public uint FocusSettings;

        [NativeTypeName("unsigned int")]
        public uint AF_C_Settings;

        public ushort FocusWarning;

        [NativeTypeName("ushort[3]")]
        public _ImageStabilization_e__FixedBuffer ImageStabilization;

        public ushort FlashMode;

        public ushort WB_Preset;

        public ushort ShutterType;

        public ushort ExrMode;

        public ushort Macro;

        [NativeTypeName("unsigned int")]
        public uint Rating;

        public ushort CropMode;

        [NativeTypeName("char[13]")]
        public _SerialSignature_e__FixedBuffer SerialSignature;

        [NativeTypeName("char[5]")]
        public _SensorID_e__FixedBuffer SensorID;

        [NativeTypeName("char[5]")]
        public _RAFVersion_e__FixedBuffer RAFVersion;

        public int RAFDataGeneration;

        public ushort RAFDataVersion;

        public int isTSNERDTS;

        public short DriveMode;

        [NativeTypeName("ushort[9]")]
        public _BlackLevel_e__FixedBuffer BlackLevel;

        [NativeTypeName("unsigned int[32]")]
        public _RAFData_ImageSizeTable_e__FixedBuffer RAFData_ImageSizeTable;

        public int AutoBracketing;

        public int SequenceNumber;

        public int SeriesLength;

        [NativeTypeName("float[2]")]
        public _PixelShiftOffset_e__FixedBuffer PixelShiftOffset;

        public int ImageCount;

        [InlineArray(33)]
        public partial struct _FujiModel_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(33)]
        public partial struct _FujiModel2_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(2)]
        public partial struct _FocusPixel_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(3)]
        public partial struct _ImageStabilization_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(13)]
        public partial struct _SerialSignature_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(5)]
        public partial struct _SensorID_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(5)]
        public partial struct _RAFVersion_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(9)]
        public partial struct _BlackLevel_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(32)]
        public partial struct _RAFData_ImageSizeTable_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(2)]
        public partial struct _PixelShiftOffset_e__FixedBuffer
        {
            public float e0;
        }
    }

    public partial struct libraw_sensor_highspeed_crop_t
    {
        public ushort cleft;

        public ushort ctop;

        public ushort cwidth;

        public ushort cheight;
    }

    public unsafe partial struct libraw_nikon_makernotes_t
    {
        public double ExposureBracketValue;

        public ushort ActiveDLighting;

        public ushort ShootingMode;

        [NativeTypeName("uchar[7]")]
        public _ImageStabilization_e__FixedBuffer ImageStabilization;

        [NativeTypeName("uchar")]
        public byte VibrationReduction;

        [NativeTypeName("uchar")]
        public byte VRMode;

        [NativeTypeName("char[13]")]
        public _FlashSetting_e__FixedBuffer FlashSetting;

        [NativeTypeName("char[20]")]
        public _FlashType_e__FixedBuffer FlashType;

        [NativeTypeName("uchar[4]")]
        public _FlashExposureCompensation_e__FixedBuffer FlashExposureCompensation;

        [NativeTypeName("uchar[4]")]
        public _ExternalFlashExposureComp_e__FixedBuffer ExternalFlashExposureComp;

        [NativeTypeName("uchar[4]")]
        public _FlashExposureBracketValue_e__FixedBuffer FlashExposureBracketValue;

        [NativeTypeName("uchar")]
        public byte FlashMode;

        [NativeTypeName("signed char")]
        public sbyte FlashExposureCompensation2;

        [NativeTypeName("signed char")]
        public sbyte FlashExposureCompensation3;

        [NativeTypeName("signed char")]
        public sbyte FlashExposureCompensation4;

        [NativeTypeName("uchar")]
        public byte FlashSource;

        [NativeTypeName("uchar[2]")]
        public _FlashFirmware_e__FixedBuffer FlashFirmware;

        [NativeTypeName("uchar")]
        public byte ExternalFlashFlags;

        [NativeTypeName("uchar")]
        public byte FlashControlCommanderMode;

        [NativeTypeName("uchar")]
        public byte FlashOutputAndCompensation;

        [NativeTypeName("uchar")]
        public byte FlashFocalLength;

        [NativeTypeName("uchar")]
        public byte FlashGNDistance;

        [NativeTypeName("uchar[4]")]
        public _FlashGroupControlMode_e__FixedBuffer FlashGroupControlMode;

        [NativeTypeName("uchar[4]")]
        public _FlashGroupOutputAndCompensation_e__FixedBuffer FlashGroupOutputAndCompensation;

        [NativeTypeName("uchar")]
        public byte FlashColorFilter;

        public ushort NEFCompression;

        public int ExposureMode;

        public int ExposureProgram;

        public int nMEshots;

        public int MEgainOn;

        [NativeTypeName("double[4]")]
        public _ME_WB_e__FixedBuffer ME_WB;

        [NativeTypeName("uchar")]
        public byte AFFineTune;

        [NativeTypeName("uchar")]
        public byte AFFineTuneIndex;

        [NativeTypeName("int8_t")]
        public sbyte AFFineTuneAdj;

        [NativeTypeName("unsigned int")]
        public uint LensDataVersion;

        [NativeTypeName("unsigned int")]
        public uint FlashInfoVersion;

        [NativeTypeName("unsigned int")]
        public uint ColorBalanceVersion;

        [NativeTypeName("uchar")]
        public byte key;

        [NativeTypeName("ushort[4]")]
        public _NEFBitDepth_e__FixedBuffer NEFBitDepth;

        public ushort HighSpeedCropFormat;

        public libraw_sensor_highspeed_crop_t SensorHighSpeedCrop;

        public ushort SensorWidth;

        public ushort SensorHeight;

        public ushort Active_D_Lighting;

        [NativeTypeName("unsigned int")]
        public uint PictureControlVersion;

        [NativeTypeName("char[20]")]
        public _PictureControlName_e__FixedBuffer PictureControlName;

        [NativeTypeName("char[20]")]
        public _PictureControlBase_e__FixedBuffer PictureControlBase;

        [NativeTypeName("unsigned int")]
        public uint ShotInfoVersion;

        [NativeTypeName("char[9]")]
        public _ShotInfoFirmware_e__FixedBuffer ShotInfoFirmware;

        [NativeTypeName("unsigned int")]
        public uint BurstTable_0x0056_len;

        [NativeTypeName("uchar *")]
        public byte* BurstTable_0x0056;

        public ushort BurstTable_0x0056_ver;

        public ushort BurstTable_0x0056_gid;

        [NativeTypeName("uchar")]
        public byte BurstTable_0x0056_fnum;

        public short MakernotesFlip;

        public double RollAngle;

        public double PitchAngle;

        public double YawAngle;

        [InlineArray(7)]
        public partial struct _ImageStabilization_e__FixedBuffer
        {
            public byte e0;
        }

        [InlineArray(13)]
        public partial struct _FlashSetting_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(20)]
        public partial struct _FlashType_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(4)]
        public partial struct _FlashExposureCompensation_e__FixedBuffer
        {
            public byte e0;
        }

        [InlineArray(4)]
        public partial struct _ExternalFlashExposureComp_e__FixedBuffer
        {
            public byte e0;
        }

        [InlineArray(4)]
        public partial struct _FlashExposureBracketValue_e__FixedBuffer
        {
            public byte e0;
        }

        [InlineArray(2)]
        public partial struct _FlashFirmware_e__FixedBuffer
        {
            public byte e0;
        }

        [InlineArray(4)]
        public partial struct _FlashGroupControlMode_e__FixedBuffer
        {
            public byte e0;
        }

        [InlineArray(4)]
        public partial struct _FlashGroupOutputAndCompensation_e__FixedBuffer
        {
            public byte e0;
        }

        [InlineArray(4)]
        public partial struct _ME_WB_e__FixedBuffer
        {
            public double e0;
        }

        [InlineArray(4)]
        public partial struct _NEFBitDepth_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(20)]
        public partial struct _PictureControlName_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(20)]
        public partial struct _PictureControlBase_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(9)]
        public partial struct _ShotInfoFirmware_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public partial struct libraw_olympus_makernotes_t
    {
        [NativeTypeName("char[6]")]
        public _CameraType2_e__FixedBuffer CameraType2;

        public ushort ValidBits;

        [NativeTypeName("unsigned int")]
        public uint tagX640;

        [NativeTypeName("unsigned int")]
        public uint tagX641;

        [NativeTypeName("unsigned int")]
        public uint tagX642;

        [NativeTypeName("unsigned int")]
        public uint tagX643;

        [NativeTypeName("unsigned int")]
        public uint tagX644;

        [NativeTypeName("unsigned int")]
        public uint tagX645;

        [NativeTypeName("unsigned int")]
        public uint tagX646;

        [NativeTypeName("unsigned int")]
        public uint tagX647;

        [NativeTypeName("unsigned int")]
        public uint tagX648;

        [NativeTypeName("unsigned int")]
        public uint tagX649;

        [NativeTypeName("unsigned int")]
        public uint tagX650;

        [NativeTypeName("unsigned int")]
        public uint tagX651;

        [NativeTypeName("unsigned int")]
        public uint tagX652;

        [NativeTypeName("unsigned int")]
        public uint tagX653;

        [NativeTypeName("int[2]")]
        public _SensorCalibration_e__FixedBuffer SensorCalibration;

        [NativeTypeName("ushort[5]")]
        public _DriveMode_e__FixedBuffer DriveMode;

        public ushort ColorSpace;

        [NativeTypeName("ushort[2]")]
        public _FocusMode_e__FixedBuffer FocusMode;

        public ushort AutoFocus;

        public ushort AFPoint;

        [NativeTypeName("unsigned int[64]")]
        public _AFAreas_e__FixedBuffer AFAreas;

        [NativeTypeName("double[5]")]
        public _AFPointSelected_e__FixedBuffer AFPointSelected;

        public ushort AFResult;

        [NativeTypeName("uchar")]
        public byte AFFineTune;

        [NativeTypeName("short[3]")]
        public _AFFineTuneAdj_e__FixedBuffer AFFineTuneAdj;

        [NativeTypeName("unsigned int[3]")]
        public _SpecialMode_e__FixedBuffer SpecialMode;

        public ushort ZoomStepCount;

        public ushort FocusStepCount;

        public ushort FocusStepInfinity;

        public ushort FocusStepNear;

        public double FocusDistance;

        [NativeTypeName("ushort[4]")]
        public _AspectFrame_e__FixedBuffer AspectFrame;

        [NativeTypeName("unsigned int[2]")]
        public _StackedImage_e__FixedBuffer StackedImage;

        [NativeTypeName("uchar")]
        public byte isLiveND;

        [NativeTypeName("unsigned int")]
        public uint LiveNDfactor;

        public ushort Panorama_mode;

        public ushort Panorama_frameNum;

        [InlineArray(6)]
        public partial struct _CameraType2_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(2)]
        public partial struct _SensorCalibration_e__FixedBuffer
        {
            public int e0;
        }

        [InlineArray(5)]
        public partial struct _DriveMode_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(2)]
        public partial struct _FocusMode_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(64)]
        public partial struct _AFAreas_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(5)]
        public partial struct _AFPointSelected_e__FixedBuffer
        {
            public double e0;
        }

        [InlineArray(3)]
        public partial struct _AFFineTuneAdj_e__FixedBuffer
        {
            public short e0;
        }

        [InlineArray(3)]
        public partial struct _SpecialMode_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(4)]
        public partial struct _AspectFrame_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(2)]
        public partial struct _StackedImage_e__FixedBuffer
        {
            public uint e0;
        }
    }

    public partial struct libraw_panasonic_makernotes_t
    {
        public ushort Compression;

        public ushort BlackLevelDim;

        [NativeTypeName("float[8]")]
        public _BlackLevel_e__FixedBuffer BlackLevel;

        [NativeTypeName("unsigned int")]
        public uint Multishot;

        public float gamma;

        [NativeTypeName("int[3]")]
        public _HighISOMultiplier_e__FixedBuffer HighISOMultiplier;

        public short FocusStepNear;

        public short FocusStepCount;

        [NativeTypeName("unsigned int")]
        public uint ZoomPosition;

        [NativeTypeName("unsigned int")]
        public uint LensManufacturer;

        [InlineArray(8)]
        public partial struct _BlackLevel_e__FixedBuffer
        {
            public float e0;
        }

        [InlineArray(3)]
        public partial struct _HighISOMultiplier_e__FixedBuffer
        {
            public int e0;
        }
    }

    public partial struct libraw_pentax_makernotes_t
    {
        [NativeTypeName("uchar[4]")]
        public _DriveMode_e__FixedBuffer DriveMode;

        [NativeTypeName("ushort[2]")]
        public _FocusMode_e__FixedBuffer FocusMode;

        [NativeTypeName("ushort[2]")]
        public _AFPointSelected_e__FixedBuffer AFPointSelected;

        public ushort AFPointSelected_Area;

        public int AFPointsInFocus_version;

        [NativeTypeName("unsigned int")]
        public uint AFPointsInFocus;

        public ushort FocusPosition;

        [NativeTypeName("uchar[4]")]
        public _DynamicRangeExpansion_e__FixedBuffer DynamicRangeExpansion;

        public short AFAdjustment;

        [NativeTypeName("uchar")]
        public byte AFPointMode;

        [NativeTypeName("uchar")]
        public byte MultiExposure;

        public ushort Quality;

        [InlineArray(4)]
        public partial struct _DriveMode_e__FixedBuffer
        {
            public byte e0;
        }

        [InlineArray(2)]
        public partial struct _FocusMode_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(2)]
        public partial struct _AFPointSelected_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(4)]
        public partial struct _DynamicRangeExpansion_e__FixedBuffer
        {
            public byte e0;
        }
    }

    public partial struct libraw_ricoh_makernotes_t
    {
        public ushort AFStatus;

        [NativeTypeName("unsigned int[2]")]
        public _AFAreaXPosition_e__FixedBuffer AFAreaXPosition;

        [NativeTypeName("unsigned int[2]")]
        public _AFAreaYPosition_e__FixedBuffer AFAreaYPosition;

        public ushort AFAreaMode;

        [NativeTypeName("unsigned int")]
        public uint SensorWidth;

        [NativeTypeName("unsigned int")]
        public uint SensorHeight;

        [NativeTypeName("unsigned int")]
        public uint CroppedImageWidth;

        [NativeTypeName("unsigned int")]
        public uint CroppedImageHeight;

        public ushort WideAdapter;

        public ushort CropMode;

        public ushort NDFilter;

        public ushort AutoBracketing;

        public ushort MacroMode;

        public ushort FlashMode;

        public double FlashExposureComp;

        public double ManualFlashOutput;

        [InlineArray(2)]
        public partial struct _AFAreaXPosition_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(2)]
        public partial struct _AFAreaYPosition_e__FixedBuffer
        {
            public uint e0;
        }
    }

    public partial struct libraw_samsung_makernotes_t
    {
        [NativeTypeName("unsigned int[4]")]
        public _ImageSizeFull_e__FixedBuffer ImageSizeFull;

        [NativeTypeName("unsigned int[4]")]
        public _ImageSizeCrop_e__FixedBuffer ImageSizeCrop;

        [NativeTypeName("int[2]")]
        public _ColorSpace_e__FixedBuffer ColorSpace;

        [NativeTypeName("unsigned int[11]")]
        public _key_e__FixedBuffer key;

        public double DigitalGain;

        public int DeviceType;

        [NativeTypeName("char[32]")]
        public _LensFirmware_e__FixedBuffer LensFirmware;

        [InlineArray(4)]
        public partial struct _ImageSizeFull_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(4)]
        public partial struct _ImageSizeCrop_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(2)]
        public partial struct _ColorSpace_e__FixedBuffer
        {
            public int e0;
        }

        [InlineArray(11)]
        public partial struct _key_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(32)]
        public partial struct _LensFirmware_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public partial struct libraw_kodak_makernotes_t
    {
        public ushort BlackLevelTop;

        public ushort BlackLevelBottom;

        public short offset_left;

        public short offset_top;

        public ushort clipBlack;

        public ushort clipWhite;

        [NativeTypeName("float[3][3]")]
        public _romm_camDaylight_e__FixedBuffer romm_camDaylight;

        [NativeTypeName("float[3][3]")]
        public _romm_camTungsten_e__FixedBuffer romm_camTungsten;

        [NativeTypeName("float[3][3]")]
        public _romm_camFluorescent_e__FixedBuffer romm_camFluorescent;

        [NativeTypeName("float[3][3]")]
        public _romm_camFlash_e__FixedBuffer romm_camFlash;

        [NativeTypeName("float[3][3]")]
        public _romm_camCustom_e__FixedBuffer romm_camCustom;

        [NativeTypeName("float[3][3]")]
        public _romm_camAuto_e__FixedBuffer romm_camAuto;

        public ushort val018percent;

        public ushort val100percent;

        public ushort val170percent;

        public short MakerNoteKodak8a;

        public float ISOCalibrationGain;

        public float AnalogISO;

        [InlineArray(3 * 3)]
        public partial struct _romm_camDaylight_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(3 * 3)]
        public partial struct _romm_camTungsten_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(3 * 3)]
        public partial struct _romm_camFluorescent_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(3 * 3)]
        public partial struct _romm_camFlash_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(3 * 3)]
        public partial struct _romm_camCustom_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(3 * 3)]
        public partial struct _romm_camAuto_e__FixedBuffer
        {
            public float e0_0;
        }
    }

    public partial struct libraw_p1_makernotes_t
    {
        [NativeTypeName("char[64]")]
        public _Software_e__FixedBuffer Software;

        [NativeTypeName("char[64]")]
        public _SystemType_e__FixedBuffer SystemType;

        [NativeTypeName("char[256]")]
        public _FirmwareString_e__FixedBuffer FirmwareString;

        [NativeTypeName("char[64]")]
        public _SystemModel_e__FixedBuffer SystemModel;

        [InlineArray(64)]
        public partial struct _Software_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _SystemType_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(256)]
        public partial struct _FirmwareString_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _SystemModel_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public partial struct libraw_sony_info_t
    {
        public ushort CameraType;

        [NativeTypeName("uchar")]
        public byte Sony0x9400_version;

        [NativeTypeName("uchar")]
        public byte Sony0x9400_ReleaseMode2;

        [NativeTypeName("unsigned int")]
        public uint Sony0x9400_SequenceImageNumber;

        [NativeTypeName("uchar")]
        public byte Sony0x9400_SequenceLength1;

        [NativeTypeName("unsigned int")]
        public uint Sony0x9400_SequenceFileNumber;

        [NativeTypeName("uchar")]
        public byte Sony0x9400_SequenceLength2;

        [NativeTypeName("uint8_t")]
        public byte AFAreaModeSetting;

        [NativeTypeName("uint16_t")]
        public ushort AFAreaMode;

        [NativeTypeName("ushort[2]")]
        public _FlexibleSpotPosition_e__FixedBuffer FlexibleSpotPosition;

        [NativeTypeName("uint8_t")]
        public byte AFPointSelected;

        [NativeTypeName("uint8_t")]
        public byte AFPointSelected_0x201e;

        public short nAFPointsUsed;

        [NativeTypeName("uint8_t[10]")]
        public _AFPointsUsed_e__FixedBuffer AFPointsUsed;

        [NativeTypeName("uint8_t")]
        public byte AFTracking;

        [NativeTypeName("uint8_t")]
        public byte AFType;

        [NativeTypeName("ushort[4]")]
        public _FocusLocation_e__FixedBuffer FocusLocation;

        public ushort FocusPosition;

        [NativeTypeName("int8_t")]
        public sbyte AFMicroAdjValue;

        [NativeTypeName("int8_t")]
        public sbyte AFMicroAdjOn;

        [NativeTypeName("uchar")]
        public byte AFMicroAdjRegisteredLenses;

        public ushort VariableLowPassFilter;

        [NativeTypeName("unsigned int")]
        public uint LongExposureNoiseReduction;

        public ushort HighISONoiseReduction;

        [NativeTypeName("ushort[2]")]
        public _HDR_e__FixedBuffer HDR;

        public ushort group2010;

        public ushort group9050;

        public ushort len_group9050;

        public ushort real_iso_offset;

        public ushort MeteringMode_offset;

        public ushort ExposureProgram_offset;

        public ushort ReleaseMode2_offset;

        [NativeTypeName("unsigned int")]
        public uint MinoltaCamID;

        public float firmware;

        public ushort ImageCount3_offset;

        [NativeTypeName("unsigned int")]
        public uint ImageCount3;

        [NativeTypeName("unsigned int")]
        public uint ElectronicFrontCurtainShutter;

        public ushort MeteringMode2;

        [NativeTypeName("char[20]")]
        public _SonyDateTime_e__FixedBuffer SonyDateTime;

        [NativeTypeName("unsigned int")]
        public uint ShotNumberSincePowerUp;

        public ushort PixelShiftGroupPrefix;

        [NativeTypeName("unsigned int")]
        public uint PixelShiftGroupID;

        [NativeTypeName("char")]
        public sbyte nShotsInPixelShiftGroup;

        [NativeTypeName("char")]
        public sbyte numInPixelShiftGroup;

        public ushort prd_ImageHeight;

        public ushort prd_ImageWidth;

        public ushort prd_Total_bps;

        public ushort prd_Active_bps;

        public ushort prd_StorageMethod;

        public ushort prd_BayerPattern;

        public ushort SonyRawFileType;

        public ushort RAWFileType;

        public ushort RawSizeType;

        [NativeTypeName("unsigned int")]
        public uint Quality;

        public ushort FileFormat;

        [NativeTypeName("char[16]")]
        public _MetaVersion_e__FixedBuffer MetaVersion;

        public float AspectRatio;

        [InlineArray(2)]
        public partial struct _FlexibleSpotPosition_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(10)]
        public partial struct _AFPointsUsed_e__FixedBuffer
        {
            public byte e0;
        }

        [InlineArray(4)]
        public partial struct _FocusLocation_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(2)]
        public partial struct _HDR_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(20)]
        public partial struct _SonyDateTime_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(16)]
        public partial struct _MetaVersion_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public unsafe partial struct libraw_colordata_t
    {
        [NativeTypeName("ushort[65536]")]
        public _curve_e__FixedBuffer curve;

        [NativeTypeName("unsigned int[4104]")]
        public _cblack_e__FixedBuffer cblack;

        [NativeTypeName("unsigned int")]
        public uint black;

        [NativeTypeName("unsigned int")]
        public uint data_maximum;

        [NativeTypeName("unsigned int")]
        public uint maximum;

        [NativeTypeName("unsigned int[4]")]
        public _linear_max_e__FixedBuffer linear_max;

        public float fmaximum;

        public float fnorm;

        [NativeTypeName("ushort[8][8]")]
        public _white_e__FixedBuffer white;

        [NativeTypeName("float[4]")]
        public _cam_mul_e__FixedBuffer cam_mul;

        [NativeTypeName("float[4]")]
        public _pre_mul_e__FixedBuffer pre_mul;

        [NativeTypeName("float[3][4]")]
        public _cmatrix_e__FixedBuffer cmatrix;

        [NativeTypeName("float[3][4]")]
        public _ccm_e__FixedBuffer ccm;

        [NativeTypeName("float[3][4]")]
        public _rgb_cam_e__FixedBuffer rgb_cam;

        [NativeTypeName("float[4][3]")]
        public _cam_xyz_e__FixedBuffer cam_xyz;

        [NativeTypeName("struct ph1_t")]
        public ph1_t phase_one_data;

        public float flash_used;

        public float canon_ev;

        [NativeTypeName("char[64]")]
        public _model2_e__FixedBuffer model2;

        [NativeTypeName("char[64]")]
        public _UniqueCameraModel_e__FixedBuffer UniqueCameraModel;

        [NativeTypeName("char[64]")]
        public _LocalizedCameraModel_e__FixedBuffer LocalizedCameraModel;

        [NativeTypeName("char[64]")]
        public _ImageUniqueID_e__FixedBuffer ImageUniqueID;

        [NativeTypeName("char[17]")]
        public _RawDataUniqueID_e__FixedBuffer RawDataUniqueID;

        [NativeTypeName("char[64]")]
        public _OriginalRawFileName_e__FixedBuffer OriginalRawFileName;

        public void* profile;

        [NativeTypeName("unsigned int")]
        public uint profile_length;

        [NativeTypeName("unsigned int[8]")]
        public _black_stat_e__FixedBuffer black_stat;

        [NativeTypeName("libraw_dng_color_t[2]")]
        public _dng_color_e__FixedBuffer dng_color;

        public libraw_dng_levels_t dng_levels;

        [NativeTypeName("int[256][4]")]
        public _WB_Coeffs_e__FixedBuffer WB_Coeffs;

        [NativeTypeName("float[64][5]")]
        public _WBCT_Coeffs_e__FixedBuffer WBCT_Coeffs;

        public int as_shot_wb_applied;

        [NativeTypeName("libraw_P1_color_t[2]")]
        public _P1_color_e__FixedBuffer P1_color;

        [NativeTypeName("unsigned int")]
        public uint raw_bps;

        public int ExifColorSpace;

        [InlineArray(65536)]
        public partial struct _curve_e__FixedBuffer
        {
            public ushort e0;
        }

        [InlineArray(4104)]
        public partial struct _cblack_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(4)]
        public partial struct _linear_max_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(8 * 8)]
        public partial struct _white_e__FixedBuffer
        {
            public ushort e0_0;
        }

        [InlineArray(4)]
        public partial struct _cam_mul_e__FixedBuffer
        {
            public float e0;
        }

        [InlineArray(4)]
        public partial struct _pre_mul_e__FixedBuffer
        {
            public float e0;
        }

        [InlineArray(3 * 4)]
        public partial struct _cmatrix_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(3 * 4)]
        public partial struct _ccm_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(3 * 4)]
        public partial struct _rgb_cam_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(4 * 3)]
        public partial struct _cam_xyz_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(64)]
        public partial struct _model2_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _UniqueCameraModel_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _LocalizedCameraModel_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _ImageUniqueID_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(17)]
        public partial struct _RawDataUniqueID_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _OriginalRawFileName_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(8)]
        public partial struct _black_stat_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(2)]
        public partial struct _dng_color_e__FixedBuffer
        {
            public libraw_dng_color_t e0;
        }

        [InlineArray(256 * 4)]
        public partial struct _WB_Coeffs_e__FixedBuffer
        {
            public int e0_0;
        }

        [InlineArray(64 * 5)]
        public partial struct _WBCT_Coeffs_e__FixedBuffer
        {
            public float e0_0;
        }

        [InlineArray(2)]
        public partial struct _P1_color_e__FixedBuffer
        {
            public libraw_P1_color_t e0;
        }
    }

    public unsafe partial struct libraw_thumbnail_t
    {
        [NativeTypeName("enum LibRaw_thumbnail_formats")]
        public LibRaw_thumbnail_formats tformat;

        public ushort twidth;

        public ushort theight;

        [NativeTypeName("unsigned int")]
        public uint tlength;

        public int tcolors;

        [NativeTypeName("char *")]
        public sbyte* thumb;
    }

    public partial struct libraw_thumbnail_item_t
    {
        [NativeTypeName("enum LibRaw_internal_thumbnail_formats")]
        public LibRaw_internal_thumbnail_formats tformat;

        public ushort twidth;

        public ushort theight;

        public ushort tflip;

        [NativeTypeName("unsigned int")]
        public uint tlength;

        [NativeTypeName("unsigned int")]
        public uint tmisc;

        [NativeTypeName("INT64")]
        public long toffset;
    }

    public partial struct libraw_thumbnail_list_t
    {
        public int thumbcount;

        [NativeTypeName("libraw_thumbnail_item_t[8]")]
        public _thumblist_e__FixedBuffer thumblist;

        [InlineArray(8)]
        public partial struct _thumblist_e__FixedBuffer
        {
            public libraw_thumbnail_item_t e0;
        }
    }

    public partial struct libraw_gps_info_t
    {
        [NativeTypeName("float[3]")]
        public _latitude_e__FixedBuffer latitude;

        [NativeTypeName("float[3]")]
        public _longitude_e__FixedBuffer longitude;

        [NativeTypeName("float[3]")]
        public _gpstimestamp_e__FixedBuffer gpstimestamp;

        public float altitude;

        [NativeTypeName("char")]
        public sbyte altref;

        [NativeTypeName("char")]
        public sbyte latref;

        [NativeTypeName("char")]
        public sbyte longref;

        [NativeTypeName("char")]
        public sbyte gpsstatus;

        [NativeTypeName("char")]
        public sbyte gpsparsed;

        [InlineArray(3)]
        public partial struct _latitude_e__FixedBuffer
        {
            public float e0;
        }

        [InlineArray(3)]
        public partial struct _longitude_e__FixedBuffer
        {
            public float e0;
        }

        [InlineArray(3)]
        public partial struct _gpstimestamp_e__FixedBuffer
        {
            public float e0;
        }
    }

    public partial struct libraw_imgother_t
    {
        public float iso_speed;

        public float shutter;

        public float aperture;

        public float focal_len;

        [NativeTypeName("time_t")]
        public long timestamp;

        [NativeTypeName("unsigned int")]
        public uint shot_order;

        [NativeTypeName("unsigned int[32]")]
        public _gpsdata_e__FixedBuffer gpsdata;

        public libraw_gps_info_t parsed_gps;

        [NativeTypeName("char[512]")]
        public _desc_e__FixedBuffer desc;

        [NativeTypeName("char[64]")]
        public _artist_e__FixedBuffer artist;

        [NativeTypeName("float[4]")]
        public _analogbalance_e__FixedBuffer analogbalance;

        [InlineArray(32)]
        public partial struct _gpsdata_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(512)]
        public partial struct _desc_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _artist_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(4)]
        public partial struct _analogbalance_e__FixedBuffer
        {
            public float e0;
        }
    }

    public unsafe partial struct libraw_afinfo_item_t
    {
        [NativeTypeName("unsigned int")]
        public uint AFInfoData_tag;

        public short AFInfoData_order;

        [NativeTypeName("unsigned int")]
        public uint AFInfoData_version;

        [NativeTypeName("unsigned int")]
        public uint AFInfoData_length;

        [NativeTypeName("uchar *")]
        public byte* AFInfoData;
    }

    public partial struct libraw_metadata_common_t
    {
        public float FlashEC;

        public float FlashGN;

        public float CameraTemperature;

        public float SensorTemperature;

        public float SensorTemperature2;

        public float LensTemperature;

        public float AmbientTemperature;

        public float BatteryTemperature;

        public float exifAmbientTemperature;

        public float exifHumidity;

        public float exifPressure;

        public float exifWaterDepth;

        public float exifAcceleration;

        public float exifCameraElevationAngle;

        public float real_ISO;

        public float exifExposureIndex;

        public ushort ColorSpace;

        [NativeTypeName("char[128]")]
        public _firmware_e__FixedBuffer firmware;

        public float ExposureCalibrationShift;

        [NativeTypeName("libraw_afinfo_item_t[4]")]
        public _afdata_e__FixedBuffer afdata;

        public int afcount;

        [InlineArray(128)]
        public partial struct _firmware_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(4)]
        public partial struct _afdata_e__FixedBuffer
        {
            public libraw_afinfo_item_t e0;
        }
    }

    public unsafe partial struct libraw_output_params_t
    {
        [NativeTypeName("unsigned int[4]")]
        public _greybox_e__FixedBuffer greybox;

        [NativeTypeName("unsigned int[4]")]
        public _cropbox_e__FixedBuffer cropbox;

        [NativeTypeName("double[4]")]
        public _aber_e__FixedBuffer aber;

        [NativeTypeName("double[6]")]
        public _gamm_e__FixedBuffer gamm;

        [NativeTypeName("float[4]")]
        public _user_mul_e__FixedBuffer user_mul;

        public float bright;

        public float threshold;

        public int half_size;

        public int four_color_rgb;

        public int highlight;

        public int use_auto_wb;

        public int use_camera_wb;

        public int use_camera_matrix;

        public int output_color;

        [NativeTypeName("char *")]
        public sbyte* output_profile;

        [NativeTypeName("char *")]
        public sbyte* camera_profile;

        [NativeTypeName("char *")]
        public sbyte* bad_pixels;

        [NativeTypeName("char *")]
        public sbyte* dark_frame;

        public int output_bps;

        public int output_tiff;

        public int output_flags;

        public int user_flip;

        public int user_qual;

        public int user_black;

        [NativeTypeName("int[4]")]
        public _user_cblack_e__FixedBuffer user_cblack;

        public int user_sat;

        public int med_passes;

        public float auto_bright_thr;

        public float adjust_maximum_thr;

        public int no_auto_bright;

        public int use_fuji_rotate;

        public int use_p1_correction;

        public int green_matching;

        public int dcb_iterations;

        public int dcb_enhance_fl;

        public int fbdd_noiserd;

        public int exp_correc;

        public float exp_shift;

        public float exp_preser;

        public int no_auto_scale;

        public int no_interpolation;

        [InlineArray(4)]
        public partial struct _greybox_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(4)]
        public partial struct _cropbox_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(4)]
        public partial struct _aber_e__FixedBuffer
        {
            public double e0;
        }

        [InlineArray(6)]
        public partial struct _gamm_e__FixedBuffer
        {
            public double e0;
        }

        [InlineArray(4)]
        public partial struct _user_mul_e__FixedBuffer
        {
            public float e0;
        }

        [InlineArray(4)]
        public partial struct _user_cblack_e__FixedBuffer
        {
            public int e0;
        }
    }

    public unsafe partial struct libraw_raw_unpack_params_t
    {
        public int use_rawspeed;

        public int use_dngsdk;

        [NativeTypeName("unsigned int")]
        public uint options;

        [NativeTypeName("unsigned int")]
        public uint shot_select;

        [NativeTypeName("unsigned int")]
        public uint specials;

        [NativeTypeName("unsigned int")]
        public uint max_raw_memory_mb;

        public int sony_arw2_posterization_thr;

        public float coolscan_nef_gamma;

        [NativeTypeName("char[5]")]
        public _p4shot_order_e__FixedBuffer p4shot_order;

        [NativeTypeName("char **")]
        public sbyte** custom_camera_strings;

        [InlineArray(5)]
        public partial struct _p4shot_order_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public unsafe partial struct libraw_rawdata_t
    {
        public void* raw_alloc;

        public ushort* raw_image;

        [NativeTypeName("ushort (*)[4]")]
        public ushort* color4_image;

        [NativeTypeName("ushort (*)[3]")]
        public ushort* color3_image;

        public float* float_image;

        [NativeTypeName("float (*)[3]")]
        public float* float3_image;

        [NativeTypeName("float (*)[4]")]
        public float* float4_image;

        [NativeTypeName("short (*)[2]")]
        public short* ph1_cblack;

        [NativeTypeName("short (*)[2]")]
        public short* ph1_rblack;

        public libraw_iparams_t iparams;

        public libraw_image_sizes_t sizes;

        public libraw_internal_output_params_t ioparams;

        public libraw_colordata_t color;
    }

    public partial struct libraw_makernotes_lens_t
    {
        [NativeTypeName("UINT64")]
        public ulong LensID;

        [NativeTypeName("char[128]")]
        public _Lens_e__FixedBuffer Lens;

        public ushort LensFormat;

        public ushort LensMount;

        [NativeTypeName("UINT64")]
        public ulong CamID;

        public ushort CameraFormat;

        public ushort CameraMount;

        [NativeTypeName("char[64]")]
        public _body_e__FixedBuffer body;

        public short FocalType;

        [NativeTypeName("char[16]")]
        public _LensFeatures_pre_e__FixedBuffer LensFeatures_pre;

        [NativeTypeName("char[16]")]
        public _LensFeatures_suf_e__FixedBuffer LensFeatures_suf;

        public float MinFocal;

        public float MaxFocal;

        public float MaxAp4MinFocal;

        public float MaxAp4MaxFocal;

        public float MinAp4MinFocal;

        public float MinAp4MaxFocal;

        public float MaxAp;

        public float MinAp;

        public float CurFocal;

        public float CurAp;

        public float MaxAp4CurFocal;

        public float MinAp4CurFocal;

        public float MinFocusDistance;

        public float FocusRangeIndex;

        public float LensFStops;

        [NativeTypeName("UINT64")]
        public ulong TeleconverterID;

        [NativeTypeName("char[128]")]
        public _Teleconverter_e__FixedBuffer Teleconverter;

        [NativeTypeName("UINT64")]
        public ulong AdapterID;

        [NativeTypeName("char[128]")]
        public _Adapter_e__FixedBuffer Adapter;

        [NativeTypeName("UINT64")]
        public ulong AttachmentID;

        [NativeTypeName("char[128]")]
        public _Attachment_e__FixedBuffer Attachment;

        public ushort FocalUnits;

        public float FocalLengthIn35mmFormat;

        [InlineArray(128)]
        public partial struct _Lens_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _body_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(16)]
        public partial struct _LensFeatures_pre_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(16)]
        public partial struct _LensFeatures_suf_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(128)]
        public partial struct _Teleconverter_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(128)]
        public partial struct _Adapter_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(128)]
        public partial struct _Attachment_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public partial struct libraw_nikonlens_t
    {
        public float EffectiveMaxAp;

        [NativeTypeName("uchar")]
        public byte LensIDNumber;

        [NativeTypeName("uchar")]
        public byte LensFStops;

        [NativeTypeName("uchar")]
        public byte MCUVersion;

        [NativeTypeName("uchar")]
        public byte LensType;
    }

    public partial struct libraw_dnglens_t
    {
        public float MinFocal;

        public float MaxFocal;

        public float MaxAp4MinFocal;

        public float MaxAp4MaxFocal;
    }

    public partial struct libraw_lensinfo_t
    {
        public float MinFocal;

        public float MaxFocal;

        public float MaxAp4MinFocal;

        public float MaxAp4MaxFocal;

        public float EXIF_MaxAp;

        [NativeTypeName("char[128]")]
        public _LensMake_e__FixedBuffer LensMake;

        [NativeTypeName("char[128]")]
        public _Lens_e__FixedBuffer Lens;

        [NativeTypeName("char[128]")]
        public _LensSerial_e__FixedBuffer LensSerial;

        [NativeTypeName("char[128]")]
        public _InternalLensSerial_e__FixedBuffer InternalLensSerial;

        public ushort FocalLengthIn35mmFormat;

        public libraw_nikonlens_t nikon;

        public libraw_dnglens_t dng;

        public libraw_makernotes_lens_t makernotes;

        [InlineArray(128)]
        public partial struct _LensMake_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(128)]
        public partial struct _Lens_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(128)]
        public partial struct _LensSerial_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(128)]
        public partial struct _InternalLensSerial_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public partial struct libraw_makernotes_t
    {
        public libraw_canon_makernotes_t canon;

        public libraw_nikon_makernotes_t nikon;

        public libraw_hasselblad_makernotes_t hasselblad;

        public libraw_fuji_info_t fuji;

        public libraw_olympus_makernotes_t olympus;

        public libraw_sony_info_t sony;

        public libraw_kodak_makernotes_t kodak;

        public libraw_panasonic_makernotes_t panasonic;

        public libraw_pentax_makernotes_t pentax;

        public libraw_p1_makernotes_t phaseone;

        public libraw_ricoh_makernotes_t ricoh;

        public libraw_samsung_makernotes_t samsung;

        public libraw_metadata_common_t common;
    }

    public partial struct libraw_shootinginfo_t
    {
        public short DriveMode;

        public short FocusMode;

        public short MeteringMode;

        public short AFPoint;

        public short ExposureMode;

        public short ExposureProgram;

        public short ImageStabilization;

        [NativeTypeName("char[64]")]
        public _BodySerial_e__FixedBuffer BodySerial;

        [NativeTypeName("char[64]")]
        public _InternalBodySerial_e__FixedBuffer InternalBodySerial;

        [InlineArray(64)]
        public partial struct _BodySerial_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _InternalBodySerial_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public partial struct libraw_custom_camera_t
    {
        [NativeTypeName("unsigned int")]
        public uint fsize;

        public ushort rw;

        public ushort rh;

        [NativeTypeName("uchar")]
        public byte lm;

        [NativeTypeName("uchar")]
        public byte tm;

        [NativeTypeName("uchar")]
        public byte rm;

        [NativeTypeName("uchar")]
        public byte bm;

        public ushort lf;

        [NativeTypeName("uchar")]
        public byte cf;

        [NativeTypeName("uchar")]
        public byte max;

        [NativeTypeName("uchar")]
        public byte flags;

        [NativeTypeName("char[10]")]
        public _t_make_e__FixedBuffer t_make;

        [NativeTypeName("char[20]")]
        public _t_model_e__FixedBuffer t_model;

        public ushort offset;

        [InlineArray(10)]
        public partial struct _t_make_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(20)]
        public partial struct _t_model_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public unsafe partial struct libraw_data_t
    {
        [NativeTypeName("ushort (*)[4]")]
        public ushort* image;

        public libraw_image_sizes_t sizes;

        public libraw_iparams_t idata;

        public libraw_lensinfo_t lens;

        public libraw_makernotes_t makernotes;

        public libraw_shootinginfo_t shootinginfo;

        public libraw_output_params_t @params;

        public libraw_raw_unpack_params_t rawparams;

        [NativeTypeName("unsigned int")]
        public uint progress_flags;

        [NativeTypeName("unsigned int")]
        public uint process_warnings;

        public libraw_colordata_t color;

        public libraw_imgother_t other;

        public libraw_thumbnail_t thumbnail;

        public libraw_thumbnail_list_t thumbs_list;

        public libraw_rawdata_t rawdata;

        public void* parent_class;
    }

    public unsafe partial struct fuji_q_table
    {
        [NativeTypeName("int8_t *")]
        public sbyte* q_table;

        public int raw_bits;

        public int total_values;

        public int max_grad;

        public int q_grad_mult;

        public int q_base;
    }

    public unsafe partial struct fuji_compressed_params
    {
        [NativeTypeName("struct fuji_q_table[4]")]
        public _qt_e__FixedBuffer qt;

        public void* buf;

        public int max_bits;

        public int min_value;

        public int max_value;

        public ushort line_width;

        [InlineArray(4)]
        public partial struct _qt_e__FixedBuffer
        {
            public fuji_q_table e0;
        }
    }

    public static unsafe partial class Methods
    {
        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void default_data_callback(void* data, [NativeTypeName("const char *")] sbyte* file, [NativeTypeName("const INT64")] long offset);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* libraw_strerror(int errorcode);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* libraw_strprogress([NativeTypeName("enum LibRaw_progress")] LibRaw_progress param0);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern libraw_data_t* libraw_init([NativeTypeName("unsigned int")] uint flags);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_open_buffer(libraw_data_t* param0, [NativeTypeName("const void *")] void* buffer, [NativeTypeName("size_t")] nuint size);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_open_bayer(libraw_data_t* lr, [NativeTypeName("unsigned char *")] byte* data, [NativeTypeName("unsigned int")] uint datalen, ushort _raw_width, ushort _raw_height, ushort _left_margin, ushort _top_margin, ushort _right_margin, ushort _bottom_margin, [NativeTypeName("unsigned char")] byte procflags, [NativeTypeName("unsigned char")] byte bayer_battern, [NativeTypeName("unsigned int")] uint unused_bits, [NativeTypeName("unsigned int")] uint otherflags, [NativeTypeName("unsigned int")] uint black_level);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_unpack(libraw_data_t* param0);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_unpack_thumb(libraw_data_t* param0);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_unpack_thumb_ex(libraw_data_t* param0, int param1);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_recycle_datastream(libraw_data_t* param0);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_recycle(libraw_data_t* param0);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_close(libraw_data_t* param0);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_subtract_black(libraw_data_t* param0);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_raw2image(libraw_data_t* param0);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_free_image(libraw_data_t* param0);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* libraw_version();

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_versionNumber();

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char **")]
        public static extern sbyte** libraw_cameraList();

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_cameraCount();

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_exifparser_handler(libraw_data_t* param0, [NativeTypeName("exif_parser_callback")] delegate* unmanaged[Cdecl]<void*, int, int, int, uint, void*, long, void> cb, void* datap);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_makernotes_handler(libraw_data_t* param0, [NativeTypeName("exif_parser_callback")] delegate* unmanaged[Cdecl]<void*, int, int, int, uint, void*, long, void> cb, void* datap);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_dataerror_handler(libraw_data_t* param0, [NativeTypeName("data_callback")] delegate* unmanaged[Cdecl]<void*, sbyte*, long, void> func, void* datap);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_progress_handler(libraw_data_t* param0, [NativeTypeName("progress_callback")] delegate* unmanaged[Cdecl]<void*, LibRaw_progress, int, int, int> cb, void* datap);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* libraw_unpack_function_name(libraw_data_t* lr);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_get_decoder_info(libraw_data_t* lr, libraw_decoder_info_t* d);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_COLOR(libraw_data_t* param0, int row, int col);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned int")]
        public static extern uint libraw_capabilities();

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_adjust_to_raw_inset_crop(libraw_data_t* lr, [NativeTypeName("unsigned int")] uint mask, float maxcrop);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_adjust_sizes_info_only(libraw_data_t* param0);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_dcraw_ppm_tiff_writer(libraw_data_t* lr, [NativeTypeName("const char *")] sbyte* filename);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_dcraw_thumb_writer(libraw_data_t* lr, [NativeTypeName("const char *")] sbyte* fname);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_dcraw_process(libraw_data_t* lr);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern libraw_processed_image_t* libraw_dcraw_make_mem_image(libraw_data_t* lr, int* errc);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern libraw_processed_image_t* libraw_dcraw_make_mem_thumb(libraw_data_t* lr, int* errc);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_dcraw_clear_mem(libraw_processed_image_t* param0);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_demosaic(libraw_data_t* lr, int value);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_output_color(libraw_data_t* lr, int value);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_adjust_maximum_thr(libraw_data_t* lr, float value);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_user_mul(libraw_data_t* lr, int index, float val);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_output_bps(libraw_data_t* lr, int value);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_gamma(libraw_data_t* lr, int index, float value);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_no_auto_bright(libraw_data_t* lr, int value);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_bright(libraw_data_t* lr, float value);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_highlight(libraw_data_t* lr, int value);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_fbdd_noiserd(libraw_data_t* lr, int value);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_get_raw_height(libraw_data_t* lr);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_get_raw_width(libraw_data_t* lr);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_get_iheight(libraw_data_t* lr);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_get_iwidth(libraw_data_t* lr);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float libraw_get_cam_mul(libraw_data_t* lr, int index);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float libraw_get_pre_mul(libraw_data_t* lr, int index);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float libraw_get_rgb_cam(libraw_data_t* lr, int index1, int index2);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int libraw_get_color_maximum(libraw_data_t* lr);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libraw_set_output_tif(libraw_data_t* lr, int value);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern libraw_iparams_t* libraw_get_iparams(libraw_data_t* lr);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern libraw_lensinfo_t* libraw_get_lensinfo(libraw_data_t* lr);

        [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern libraw_imgother_t* libraw_get_imgother(libraw_data_t* lr);

        [NativeTypeName("#define LIBRAW_DEFAULT_ADJUST_MAXIMUM_THRESHOLD 0.75f")]
        public const float LIBRAW_DEFAULT_ADJUST_MAXIMUM_THRESHOLD = 0.75f;

        [NativeTypeName("#define LIBRAW_DEFAULT_AUTO_BRIGHTNESS_THRESHOLD 0.01f")]
        public const float LIBRAW_DEFAULT_AUTO_BRIGHTNESS_THRESHOLD = 0.01f;

        [NativeTypeName("#define LIBRAW_MAX_ALLOC_MB_DEFAULT 2048L")]
        public const int LIBRAW_MAX_ALLOC_MB_DEFAULT = 2048;

        [NativeTypeName("#define LIBRAW_MAX_PROFILE_SIZE_MB 256LL")]
        public const long LIBRAW_MAX_PROFILE_SIZE_MB = 256L;

        [NativeTypeName("#define LIBRAW_MAX_NONDNG_RAW_FILE_SIZE 2147483647LL")]
        public const long LIBRAW_MAX_NONDNG_RAW_FILE_SIZE = 2147483647L;

        [NativeTypeName("#define LIBRAW_MAX_CR3_RAW_FILE_SIZE LIBRAW_MAX_NONDNG_RAW_FILE_SIZE")]
        public const long LIBRAW_MAX_CR3_RAW_FILE_SIZE = 2147483647L;

        [NativeTypeName("#define LIBRAW_MAX_DNG_RAW_FILE_SIZE 2147483647LL")]
        public const long LIBRAW_MAX_DNG_RAW_FILE_SIZE = 2147483647L;

        [NativeTypeName("#define LIBRAW_MAX_THUMBNAIL_MB 512L")]
        public const int LIBRAW_MAX_THUMBNAIL_MB = 512;

        [NativeTypeName("#define LIBRAW_MAX_METADATA_BLOCKS 1024")]
        public const int LIBRAW_MAX_METADATA_BLOCKS = 1024;

        [NativeTypeName("#define LIBRAW_CBLACK_SIZE 4104")]
        public const int LIBRAW_CBLACK_SIZE = 4104;

        [NativeTypeName("#define LIBRAW_IFD_MAXCOUNT 10")]
        public const int LIBRAW_IFD_MAXCOUNT = 10;

        [NativeTypeName("#define LIBRAW_THUMBNAIL_MAXCOUNT 8")]
        public const int LIBRAW_THUMBNAIL_MAXCOUNT = 8;

        [NativeTypeName("#define LIBRAW_CRXTRACKS_MAXCOUNT 16")]
        public const int LIBRAW_CRXTRACKS_MAXCOUNT = 16;

        [NativeTypeName("#define LIBRAW_AFDATA_MAXCOUNT 4")]
        public const int LIBRAW_AFDATA_MAXCOUNT = 4;

        [NativeTypeName("#define LIBRAW_AHD_TILE 512")]
        public const int LIBRAW_AHD_TILE = 512;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_int8u LIBRAW_EXIFTAG_TYPE_BYTE")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_int8u = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_BYTE;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_string LIBRAW_EXIFTAG_TYPE_ASCII")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_string = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_ASCII;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_int16u LIBRAW_EXIFTAG_TYPE_SHORT")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_int16u = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_SHORT;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_int32u LIBRAW_EXIFTAG_TYPE_LONG")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_int32u = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_LONG;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_rational64u LIBRAW_EXIFTAG_TYPE_RATIONAL")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_rational64u = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_RATIONAL;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_int8s LIBRAW_EXIFTAG_TYPE_SBYTE")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_int8s = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_SBYTE;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_undef LIBRAW_EXIFTAG_TYPE_UNDEFINED")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_undef = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_UNDEFINED;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_binary LIBRAW_EXIFTAG_TYPE_UNDEFINED")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_binary = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_UNDEFINED;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_int16s LIBRAW_EXIFTAG_TYPE_SSHORT")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_int16s = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_SSHORT;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_int32s LIBRAW_EXIFTAG_TYPE_SLONG")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_int32s = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_SLONG;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_rational64s LIBRAW_EXIFTAG_TYPE_SRATIONAL")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_rational64s = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_SRATIONAL;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_float LIBRAW_EXIFTAG_TYPE_FLOAT")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_float = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_FLOAT;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_double LIBRAW_EXIFTAG_TYPE_DOUBLE")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_double = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_DOUBLE;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_ifd LIBRAW_EXIFTAG_TYPE_IFD")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_ifd = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_IFD;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_unicode LIBRAW_EXIFTAG_TYPE_UNICODE")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_unicode = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_UNICODE;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_complex LIBRAW_EXIFTAG_TYPE_COMPLEX")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_complex = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_COMPLEX;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_int64u LIBRAW_EXIFTAG_TYPE_LONG8")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_int64u = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_LONG8;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_int64s LIBRAW_EXIFTAG_TYPE_SLONG8")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_int64s = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_SLONG8;

        [NativeTypeName("#define LIBRAW_EXIFTOOLTAGTYPE_ifd64 LIBRAW_EXIFTAG_TYPE_IFD8")]
        public const LibRaw_ExifTagTypes LIBRAW_EXIFTOOLTAGTYPE_ifd64 = LibRaw_ExifTagTypes.LIBRAW_EXIFTAG_TYPE_IFD8;

        [NativeTypeName("#define LIBRAW_LENS_NOT_SET 0xffffffffffffffffULL")]
        public const ulong LIBRAW_LENS_NOT_SET = 0xffffffffffffffffUL;

        [NativeTypeName("#define LIBRAW_XTRANS 9")]
        public const int LIBRAW_XTRANS = 9;

        [NativeTypeName("#define LIBRAW_PROGRESS_THUMB_MASK 0x0fffffff")]
        public const int LIBRAW_PROGRESS_THUMB_MASK = 0x0fffffff;

        [NativeTypeName("#define LIBRAW_MAJOR_VERSION 0")]
        public const int LIBRAW_MAJOR_VERSION = 0;

        [NativeTypeName("#define LIBRAW_MINOR_VERSION 22")]
        public const int LIBRAW_MINOR_VERSION = 22;

        [NativeTypeName("#define LIBRAW_PATCH_VERSION 0")]
        public const int LIBRAW_PATCH_VERSION = 0;

        [NativeTypeName("#define LIBRAW_SHLIB_CURRENT 24")]
        public const int LIBRAW_SHLIB_CURRENT = 24;

        [NativeTypeName("#define LIBRAW_SHLIB_REVISION 0")]
        public const int LIBRAW_SHLIB_REVISION = 0;

        [NativeTypeName("#define LIBRAW_SHLIB_AGE 0")]
        public const int LIBRAW_SHLIB_AGE = 0;

        [NativeTypeName("#define LIBRAW_VERSION_STR LIBRAW_VERSION_MAKE(LIBRAW_MAJOR_VERSION, LIBRAW_MINOR_VERSION,              \\\n                      LIBRAW_PATCH_VERSION, LIBRAW_VERSION_TAIL)")]
        public static ReadOnlySpan<byte> LIBRAW_VERSION_STR => "0.22.0-Release"u8;

        [NativeTypeName("#define LIBRAW_VERSION LIBRAW_MAKE_VERSION(LIBRAW_MAJOR_VERSION, LIBRAW_MINOR_VERSION,              \\\n                      LIBRAW_PATCH_VERSION)")]
        public const int LIBRAW_VERSION = (((0) << 16) | ((22) << 8) | (0));

        [NativeTypeName("#define LibRawBigEndian 0")]
        public const int LibRawBigEndian = 0;

        [NativeTypeName("#define LIBRAW_MSIZE 512")]
        public const int LIBRAW_MSIZE = 512;
    }
}
