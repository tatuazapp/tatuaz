import {
  Modal,
  ModalOverlay,
  ModalContent,
  useDisclosure,
  Drawer,
  DrawerOverlay,
  DrawerContent,
} from "@chakra-ui/react"
import { motion } from "framer-motion"
import { useState } from "react"
import { FormattedMessage } from "react-intl"
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

  const onLikeIconClickHandler = () => {
    setTimeout(() => {
      setIsPostLiked((prev) => !prev)
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
              <motion.button
                whileTap={{ scale: 0.7, transition: { duration: 0.01 } }}
              >
                {isPostLiked ? (
                  <LikedPhotoIcon onClick={onLikeIconClickHandler} />
                ) : (
                  <NotLikedPhotoIcon onClick={onLikeIconClickHandler} />
                )}
              </motion.button>

              <LikesNumber>
                234 <FormattedMessage defaultMessage="polubień" id="1/6yup" />
              </LikesNumber>
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
              <CommentsNumber>
                234 <FormattedMessage defaultMessage="komentarzy" id="M9FmsT" />
              </CommentsNumber>
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
            backgroundColor="rgba(21, 21, 21, 0.8)"
            display="flex"
            justifyContent="center"
          >
            <ArtistPostCommentView onClose={onClose} />
          </DrawerContent>
        </Drawer>
      )}
    </>
  )
}

export default ArtistPost
