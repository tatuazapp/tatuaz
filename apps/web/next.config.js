//@ts-check

// eslint-disable-next-line @typescript-eslint/no-var-requires
const { withNx } = require("@nrwl/next/plugins/with-nx")

/**
 * @type {import('@nrwl/next/plugins/with-nx').WithNxOptions}
 **/
const nextConfig = {
  nx: {
    svgr: true,
  },
  compiler: {
    styledComponents: true,
  },
  images: {
    unoptimized: true,
    remotePatterns: [
      {
        protocol: "https",
        hostname: "**.googleusercontent.com",
      },
    ],
  },
  exportPathMap: async function (defaultPathMap, { dev }) {
    if (dev) {
      return defaultPathMap
    }

    return {
      "/": { page: "/" },
      "/404": { page: "/404" },
    }
  },
}

module.exports = withNx(nextConfig)
