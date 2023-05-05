import { Avatar, AvatarBadge } from "@chakra-ui/react"
import { useTheme } from "styled-components"
import useMe from "../../../../api/hooks/useMe"
import formatCDNImageUrl from "../../../../utils/format/formatCDNImageUrl"
import { UserSectionAreaWrapper, UserName } from "./styles"

const MAX_AVATAR_SIZE = 256

const UserSection = () => {
  const me = useMe()
  const theme = useTheme()

  return (
    <UserSectionAreaWrapper>
      <Avatar
        size="md"
        src={
          me?.foregroundPhotoUri
            ? formatCDNImageUrl(me?.foregroundPhotoUri, {
                maxHeight: MAX_AVATAR_SIZE,
                maxWidth: MAX_AVATAR_SIZE,
              })
            : ""
        }
      >
        <AvatarBadge bg={theme.colors.primary} boxSize="1em" />
      </Avatar>
      <UserName>{me?.username ?? "-"}</UserName>
    </UserSectionAreaWrapper>
  )
}

export default UserSection
