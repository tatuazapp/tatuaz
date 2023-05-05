import { removeUndefinedOrNull } from "../removeUndefinedOrNull"

type formatCDNImageUrlOptions = {
  width?: number
  maxWidth?: number
  height?: number
  maxHeight?: number
  minWidth?: number
  quality?: number
  format?: "bmp" | "gif" | "jpg" | "pbm" | "png" | "tga" | "tiff" | "webp"
}

const deviceSizes = [640, 750, 828, 1080, 1200, 1920, 2048, 3840]
const imageSizes = [16, 32, 48, 64, 96, 128, 256, 384].map((size) => size * 4)

const defaultWindowSize = 1920

const formatCDNImageUrl = (
  uri: string,
  options: formatCDNImageUrlOptions = {}
) => {
  const { width, maxWidth, minWidth, height, quality, format } = options

  const windowInnerWidth =
    typeof window !== "undefined" ? window.innerWidth : defaultWindowSize
  const windowInnerHeight =
    typeof window !== "undefined" ? window.innerHeight : defaultWindowSize

  const windowSize = Math.max(windowInnerWidth, windowInnerHeight)

  const currentDeviceSizeIndex = deviceSizes.findIndex(
    (size) => size > windowSize
  )
  const currentImageSize =
    imageSizes.at(currentDeviceSizeIndex) || imageSizes.at(-1)

  let adjustedImageWidth = currentImageSize ?? 16

  if (maxWidth) {
    adjustedImageWidth = Math.min(currentImageSize ?? 16, maxWidth)
  }

  if (width) {
    adjustedImageWidth = width
  }

  if (minWidth) {
    adjustedImageWidth = Math.max(minWidth, adjustedImageWidth)
  }

  const autoOptions = {
    width: adjustedImageWidth,
    quality: 100,
    format: "webp",
  }

  const urlParams = new URLSearchParams(
    removeUndefinedOrNull({
      width: width ? width.toString() : autoOptions.width?.toString(),
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
