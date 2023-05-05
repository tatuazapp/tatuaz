import { Heading4 } from "@tatuaz/ui"
import { FunctionComponent, useState } from "react"
import useMe from "../../../../api/hooks/useMe"
import formatCDNImageUrl from "../../../../utils/format/formatCDNImageUrl"
import AvatarUploadModal from "../AvatarUploadModal"
import {
  AvatarContainer,
  BackgroundAndAvatarContainer,
  BackgroundPhotoContainer,
  UserInfoContainer,
} from "../BackgroundAndAvatarContainer/styles"
import BackgroundPhotoUploadModal from "../BackgroundPhotoUploadModal"

const MAX_BACKGROUND_PHOTO_SIZE = 1024
const MAX_AVATAR_SIZE = 512

const BackgroundPhotoSection: FunctionComponent = () => {
  const [isBackgroundModalOpen, setIsBackgroundModalOpen] = useState(false)
  const [isAvatarModalOpen, setIsAvatarModalOpen] = useState(false)

  const handleBackgroundPhotoClick = () => {
    setIsBackgroundModalOpen(true)
  }

  const handleAvatarPhotoClick = () => {
    setIsAvatarModalOpen(true)
  }

  const me = useMe()

  return (
    <>
      <BackgroundAndAvatarContainer>
        <BackgroundPhotoContainer
          imageUrl={
            me?.backgroundPhotoUri &&
            formatCDNImageUrl(me?.backgroundPhotoUri, {
              width: MAX_BACKGROUND_PHOTO_SIZE,
            })
          }
          onClick={handleBackgroundPhotoClick}
        />
        <AvatarContainer
          src={formatCDNImageUrl(me?.foregroundPhotoUri ?? "", {
            maxWidth: MAX_AVATAR_SIZE,
          })}
          onClick={handleAvatarPhotoClick}
        />
        <UserInfoContainer>
          <Heading4>{me?.username ?? "-"}</Heading4>
        </UserInfoContainer>
      </BackgroundAndAvatarContainer>

      <BackgroundPhotoUploadModal
        isOpen={isBackgroundModalOpen}
        onClose={() => setIsBackgroundModalOpen(false)}
      />
      <AvatarUploadModal
        isOpen={isAvatarModalOpen}
        onClose={() => setIsAvatarModalOpen(false)}
      />
    </>
  )
}

export default BackgroundPhotoSection
