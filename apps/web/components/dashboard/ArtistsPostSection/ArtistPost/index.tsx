/* eslint-disable @next/next/no-img-element */
import {
  Modal,
  ModalOverlay,
  ModalContent,
  useDisclosure,
  Drawer,
  DrawerOverlay,
  DrawerContent,
} from "@chakra-ui/react"
import { useMutation } from "@tanstack/react-query"
import { motion } from "framer-motion"
import Link from "next/link"
import { FunctionComponent, useState } from "react"
import ClampLines from "react-clamp-lines"
import { FormattedMessage, useIntl } from "react-intl"
import { PhotoProvider, PhotoView } from "react-photo-view"
import { api } from "../../../../api/apiClient"
import { queryKeys } from "../../../../api/queryKeys"
import { queryClient } from "../../../../pages/_app"
import formatCDNImageUrl from "../../../../utils/format/formatCDNImageUrl"
import useIsTablet from "../../../../utils/hooks/useIsTablet"
import ArtistPostCommentView from "./ArtistPostCommentView"
import {
  ArtistPostContent,
  ArtistPostDescription,
  ArtistPostLikesAndCommentsWrapper,
  ArtistPostMainPhotos,
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
import "react-photo-view/dist/react-photo-view.css"

type ArtistPostProps = {
  id: string
  isLiked: boolean
  likesNumber: number
  commentsNumber: number
  description: string
  author: {
    name: string
    photoUri: string
  }
  createdAt?: string
  photoUris?: string[]
}

const ArtistPost: FunctionComponent<ArtistPostProps> = ({
  id,
  isLiked,
  likesNumber,
  commentsNumber,
  description,
  author,
  photoUris,
  createdAt,
}) => {
  const [isPostLiked, setIsPostLiked] = useState(isLiked)
  const [likedNumber, setLikedNumber] = useState(likesNumber)
  const [isCommentsSectionOpen, setIsCommentsSectionOpen] = useState(false)
  const intl = useIntl()

  const likePostMutation = useMutation({
    mutationFn: () =>
      api.post.likePost({
        postId: id,
        like: !isPostLiked,
      }),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [queryKeys.getUserPosts, author.name],
      })
    },
    onError: () => {
      queryClient.invalidateQueries({
        queryKey: [queryKeys.getUserPosts, author.name],
      })
    },
  })

  const onLikeIconClickHandler = () => {
    likePostMutation.mutate()
    setIsPostLiked((prev) => !prev)
    setLikedNumber((prev) => (isPostLiked ? prev - 1 : prev + 1))
  }

  const isTablet = useIsTablet()

  const { isOpen, onOpen, onClose } = useDisclosure()

  return (
    <>
      <ArtistPostWrapper>
        {photoUris && photoUris.length > 0 && (
          <ArtistPostMainPhotos>
            <PhotoProvider>
              {photoUris.map((photoUri) => (
                <PhotoView
                  key={photoUri}
                  src={formatCDNImageUrl(photoUri, {
                    maxWidth: 2048,
                    minWidth: 1024,
                  })}
                >
                  <img
                    alt={description}
                    src={formatCDNImageUrl(photoUri, {
                      maxWidth: 2048,
                      minWidth: 1024,
                    })}
                  />
                </PhotoView>
              ))}
            </PhotoProvider>
          </ArtistPostMainPhotos>
        )}
        <ArtistPostContent>
          <ArtistPostUserWrapper>
            <UserIconPhoto
              photoUrl={
                author?.photoUri
                  ? formatCDNImageUrl(author.photoUri, {
                      maxWidth: 64,
                    })
                  : ""
              }
            />
            <Link
              href={`/dashboard/profile?${new URLSearchParams({
                profileName: author.name ?? "",
              }).toString()}`}
            >
              <UserName>{author.name}</UserName>
            </Link>
          </ArtistPostUserWrapper>
          <ArtistPostDescription>
            <ClampLines
              id={description + author.photoUri + createdAt}
              lessText={intl.formatMessage({
                defaultMessage: "Mniej",
                id: "feQNnn",
              })}
              lines={4}
              moreText={intl.formatMessage({
                defaultMessage: "Pokaż więcej",
                id: "UC6hZD",
              })}
              text={description}
            />
          </ArtistPostDescription>
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
                {likedNumber}{" "}
                <FormattedMessage defaultMessage="polubień" id="1/6yup" />
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
                {commentsNumber}{" "}
                <FormattedMessage defaultMessage="komentarzy" id="M9FmsT" />
              </CommentsNumber>
            </CommentsContainer>
          </ArtistPostLikesAndCommentsWrapper>
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
