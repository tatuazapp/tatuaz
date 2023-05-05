import { Api } from "./tatuazApi"

export const api = new Api({
  baseUrl: process.env.NEXT_PUBLIC_API_URL,
  securityWorker: async (securityData: (() => Promise<string>) | null) => {
    if (!securityData) {
      return undefined
    }

    const accessToken = await securityData()

    return accessToken
      ? {
          headers: { Authorization: `Bearer ${accessToken}` },
        }
      : undefined
  },
})
