import useAccessToken from "../utils/hooks/useAccessToken"
import { Api } from "./tatuazApi"

export const api = new Api({
  baseUrl: process.env.NEXT_PUBLIC_API_URL,
  securityWorker: (accessToken) =>
    accessToken
      ? {
          headers: { Authorization: `Bearer ${accessToken}` },
        }
      : undefined,
})

export const useApiInit = () => {
  const accessToken = useAccessToken()

  if (!accessToken) return

  api.setSecurityData(accessToken)
}
