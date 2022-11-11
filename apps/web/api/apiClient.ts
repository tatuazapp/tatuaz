import { Api } from "./tatuazApi"

export const api = new Api({
  baseUrl: process.env.NEXT_PUBLIC_API_URL,
  securityWorker: async (getAccessToken: () => Promise<string>) => {
    const accessToken = await getAccessToken()

    return accessToken
      ? {
          headers: { Authorization: `Bearer ${accessToken}` },
        }
      : undefined
  },
})
