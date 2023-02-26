import { removeUndefinedOrNull } from "../removeUndefinedOrNull"

type formatCDNImageUrlOptions = {
  width?: number
  height?: number
  quality?: number
  format?: "bmp" | "gif" | "jpg" | "pbm" | "png" | "tga" | "tiff" | "webp"
}

const deviceSizes = [640, 750, 828, 1080, 1200, 1920, 2048, 3840]
const imageSizes = [16, 32, 48, 64, 96, 128, 256, 384].map((size) => size * 4)

const formatCDNImageUrl = (
  uri: string,
  options: formatCDNImageUrlOptions = {}
) => {
  const { width, height, quality, format } = options

  const windowSize = Math.max(window.innerWidth, window.innerHeight)

  const currentDeviceSizeIndex = deviceSizes.findIndex(
    (size) => size > windowSize
  )
  const currentImageSize = imageSizes[currentDeviceSizeIndex]

  const autoOptions = {
    width: currentImageSize,
    quality: 100,
    format: "webp",
  }

  const urlParams = new URLSearchParams(
    removeUndefinedOrNull({
      width: width ? width.toString() : autoOptions.width.toString(),
      height: height ? height.toString() : null,
      quality: quality ? quality.toString() : autoOptions.quality?.toString(),
      format: format || autoOptions.format,
    }) as Record<string, string>
  )

  const cdnUrl = `${
    process.env.NEXT_PUBLIC_API_URL
  }${uri}?${urlParams.toString()}`
  return cdnUrl
}

export default formatCDNImageUrl
