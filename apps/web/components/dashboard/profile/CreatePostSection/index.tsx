import { Avatar } from "@chakra-ui/react"
import { useState } from "react"
import useMe from "../../../../api/hooks/useMe"
import formatCDNImageUrl from "../../../../utils/format/formatCDNImageUrl"
import CreatePostModal from "../CreatePostModal"
import { CreatePostContainer, CreatePostInputButton } from "./styles"

const MAX_AVATAR_SIZE = 512

const CreatePostSection = () => {
  const [isCreatePostModalOpen, setIsCreatePostModalOpen] = useState(false)

  const me = useMe()

  const handleCreatePostModalOpen = () => {
    setIsCreatePostModalOpen(true)
  }

  const handleCreatePostModalClose = () => {
    setIsCreatePostModalOpen(false)
  }

  return (
    <>
      <CreatePostContainer>
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
        />
        <CreatePostInputButton
          placeholder="Make a post"
          onClick={handleCreatePostModalOpen}
        />
      </CreatePostContainer>
      <CreatePostModal
        isOpen={isCreatePostModalOpen}
        onClose={handleCreatePostModalClose}
      />
    </>
  )
}

export default CreatePostSection
