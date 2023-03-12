import { useRouter } from "next/router"
import { FormattedMessage } from "react-intl"
import Button from "../../../components/common/buttons/Button"

export const FindArtistButton = () => {
  const router = useRouter()

  return (
    <Button kind="arrowUpRight" onClick={() => router.push("/dashboard")}>
      <FormattedMessage defaultMessage="Znajdź artystę" id="okwzd8" />
    </Button>
  )
}
