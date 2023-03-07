import {
  Modal,
  ModalOverlay,
  ModalContent,
  useDisclosure,
  Drawer,
  DrawerOverlay,
  DrawerContent,
} from "@chakra-ui/react"
import { useState } from "react"
import useIsTablet from "../../../../utils/hooks/useIsTablet"
import ArtistPostCommentView from "./ArtistPostCommentView"
import {
  ArtistPostContent,
  ArtistPostDescription,
  ArtistPostLikesAndCommentsWrapper,
  ArtistPostMainPhoto,
  ArtistPostMainPhotoTitle,
  ArtistPostUserWrapper,
  ArtistPostWrapper,
  CommentsContainer,
  CommentSectionClickedIcon,
  CommentSectionNotClickedIcon,
  CommentsNumber,
  LikedPhotoIcon,
  LikesContainer,
  LikesNumber,
  NotLikedPhotoIcon,
  UserIconPhoto,
  UserName,
} from "./styles"

const ArtistPost = () => {
  const [isPostLiked, setIsPostLiked] = useState(false)
  const [isCommentsSectionOpen, setIsCommentsSectionOpen] = useState(false)

  const onNotLikedPhotoClickHandler = () => {
    const notLikedButton = document.getElementById("notLikedButton")
    notLikedButton.style.transform = "scale(0.7)"
    setTimeout(() => {
      setIsPostLiked(true)
    }, 100)
  }

  const onLikedPhotoClickHandler = () => {
    const likedButton = document.getElementById("likedButton")
    likedButton.style.transform = "scale(0.8)"
    setTimeout(() => {
      setIsPostLiked(false)
    }, 100)
  }

  const isTablet = useIsTablet()

  const { isOpen, onOpen, onClose } = useDisclosure()

  return (
    <>
      <ArtistPostWrapper>
        <ArtistPostMainPhoto>
          <ArtistPostMainPhotoTitle>Flare Boom</ArtistPostMainPhotoTitle>
        </ArtistPostMainPhoto>
        <ArtistPostContent>
          <ArtistPostLikesAndCommentsWrapper>
            <LikesContainer>
              {isPostLiked ? (
                <LikedPhotoIcon
                  id="likedButton"
                  onClick={onLikedPhotoClickHandler}
                />
              ) : (
                <NotLikedPhotoIcon
                  id="notLikedButton"
                  onClick={onNotLikedPhotoClickHandler}
                />
              )}
              <LikesNumber>234 Likes</LikesNumber>
            </LikesContainer>
            <CommentsContainer>
              {isCommentsSectionOpen ? (
                <CommentSectionClickedIcon
                  onClick={() => {
                    setIsCommentsSectionOpen(false)
                  }}
                />
              ) : (
                <CommentSectionNotClickedIcon onClick={onOpen} />
              )}
              <CommentsNumber>234 Comments</CommentsNumber>
            </CommentsContainer>
          </ArtistPostLikesAndCommentsWrapper>
          <ArtistPostUserWrapper>
            <UserIconPhoto />
            <UserName>Jacob Vin</UserName>
          </ArtistPostUserWrapper>
          <ArtistPostDescription>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua.
            Consequat ac felis donec et odio pellentesque diam volutpat commodo.
            Venenatis cras sedfelis eget. Quis hendrerit dolor magna eget est
            lorem ipsum.
          </ArtistPostDescription>
        </ArtistPostContent>
      </ArtistPostWrapper>
      {!isTablet && (
        <Modal isCentered isOpen={isOpen} size="xxl" onClose={onClose}>
          <ModalOverlay />
          <ModalContent marginBottom="10px" marginTop="10px" width="735px">
            <ArtistPostCommentView onClose={onClose} />
          </ModalContent>
        </Modal>
      )}
      {isTablet && (
        <Drawer isOpen={isOpen} placement="right" size="full" onClose={onClose}>
          <DrawerOverlay />
          <DrawerContent
            display="flex"
            justifyContent="center"
            backgroundColor="rgba(21, 21, 21, 0.8)"
          >
            <ArtistPostCommentView onClose={onClose} />
          </DrawerContent>
        </Drawer>
      )}
    </>
  )
}

export default ArtistPost