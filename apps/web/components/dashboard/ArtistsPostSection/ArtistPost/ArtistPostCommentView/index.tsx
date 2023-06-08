/* eslint-disable @next/next/no-img-element */
import { useMutation, useQuery } from "@tanstack/react-query"
import { Heading, Paragraph } from "@tatuaz/ui"
import { motion } from "framer-motion"
import { useEffect, useState } from "react"
import { FormattedMessage, useIntl } from "react-intl"
import { PhotoProvider, PhotoView } from "react-photo-view"
import { api } from "../../../../../api/apiClient"
import useMe from "../../../../../api/hooks/useMe"
import { queryKeys } from "../../../../../api/queryKeys"
import { queryClient } from "../../../../../pages/_app"
import { theme } from "../../../../../styles/theme"
import formatCDNImageUrl from "../../../../../utils/format/formatCDNImageUrl"
import {
  ArtistPostDescription,
  ArtistPostLikesAndCommentsWrapper,
  ArtistPostMainPhotos,
  CommentSectionClickedIcon,
  CommentsNumber,
  LikedPhotoIcon,
  LikesContainer,
  LikesNumber,
  NotLikedPhotoIcon,
  UserIconPhoto,
} from "../styles"
import PostComment from "./PostComment"
import {
  ArtistPostCommentsViewHeader,
  ArtistPostCommentsViewHeaderCloseButton,
  ArtistPostContent,
  ArtistPostUserWrapper,
  ArtistPostCommentsViewWrapper,
  CommentsContainer,
  ArtistPostCommentsViewCreateCommentSection,
  ArtistPostCommentsViewCreateCommentInput,
  ArtistPostCommentsViewCommentsSection,
  ArtistPostCommentsViewDivider,
  ArtistPostScrollingArea,
  ArtistPostCommentsViewCreateCommentAvatar,
} from "./styles"

type ArtistPostCommentViewProps = {
  onClose: () => void
  postId: string
}

const ArtistPostCommentView: React.FunctionComponent<
  ArtistPostCommentViewProps
> = ({ onClose, postId }) => {
  const [isPostLiked, setIsPostLiked] = useState(false)
  const [likedNumber, setLikedNumber] = useState(0)
  const [commentContent, setCommentContent] = useState("")

  const me = useMe()

  const intl = useIntl()

  const { data: postDetails } = useQuery(
    [queryKeys.getPostDetails, postId],
    () =>
      api.post.getPostDetails({
        postId,
      }),
    {}
  )

  const likePostMutation = useMutation({
    mutationFn: () =>
      api.post.likePost({
        postId: postId,
        like: !isPostLiked,
      }),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [queryKeys.getUserPosts, postDetails?.value.authorName],
      })
    },
    onError: () => {
      queryClient.invalidateQueries({
        queryKey: [queryKeys.getUserPosts, postDetails?.value.authorName],
      })
    },
  })

  const addCommentMutation = useMutation({
    mutationFn: (variables: { postId: string; content: string }) =>
      api.comment.submitComment({
        postId: variables.postId,
        content: variables.content,
        parentCommentId: null,
      }),
    onSuccess: () => {
      queryClient.invalidateQueries([queryKeys.getPostDetails, postId])
      setCommentContent("")
    },
    onError: (error) => {
      console.error(error)
    },
  })

  const onLikeIconClickHandler = () => {
    likePostMutation.mutate()
    setIsPostLiked((prev) => !prev)
    setLikedNumber((prev) => (isPostLiked ? prev - 1 : prev + 1))
  }

  const onCommentSubmitHandler = () => {
    addCommentMutation.mutate({
      postId: postId,
      content: commentContent,
    })
  }

  useEffect(() => {
    setIsPostLiked(!!postDetails?.value.isLikedByCurrentUser)
    setLikedNumber(postDetails?.value.likesCount ?? 0)
  }, [postDetails?.value.isLikedByCurrentUser, postDetails?.value.likesCount])

  return (
    <ArtistPostCommentsViewWrapper>
      <ArtistPostCommentsViewHeader>
        <Heading color={theme.colors.secondary} level={4}>
          <FormattedMessage
            defaultMessage="Post użytkownika {name}"
            id="rCeiYs"
            values={{ name: postDetails?.value.authorName }}
          />
        </Heading>
        <ArtistPostCommentsViewHeaderCloseButton onClick={onClose} />
      </ArtistPostCommentsViewHeader>
      <ArtistPostScrollingArea>
        <ArtistPostMainPhotos>
          <PhotoProvider>
            {postDetails?.value.photos.map(({ uri }) => (
              <PhotoView
                key={uri}
                src={formatCDNImageUrl(uri, {
                  maxWidth: 2048,
                  minWidth: 1024,
                })}
              >
                <img
                  alt={postDetails?.value.description}
                  src={formatCDNImageUrl(uri, {
                    maxWidth: 2048,
                    minWidth: 1024,
                  })}
                />
              </PhotoView>
            ))}
          </PhotoProvider>
        </ArtistPostMainPhotos>
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
                {likedNumber}{" "}
                <FormattedMessage defaultMessage="polubień" id="1/6yup" />
              </LikesNumber>
            </LikesContainer>
            <CommentsContainer>
              <CommentSectionClickedIcon />
              <CommentsNumber>
                <Paragraph color={theme.colors.background4} level={2}>
                  {postDetails?.value.parentComments.length}{" "}
                  <FormattedMessage defaultMessage="komentarzy" id="M9FmsT" />
                </Paragraph>
              </CommentsNumber>
            </CommentsContainer>
          </ArtistPostLikesAndCommentsWrapper>
          <ArtistPostUserWrapper>
            <UserIconPhoto
              photoUrl={
                postDetails?.value.authorPhotoUri
                  ? formatCDNImageUrl(postDetails?.value.authorPhotoUri)
                  : ""
              }
            />

            <Paragraph strong color={theme.colors.primary} level={2}>
              Jacob Vin
            </Paragraph>
          </ArtistPostUserWrapper>
          <ArtistPostDescription>
            <Paragraph level={2}>{postDetails?.value.description}</Paragraph>
          </ArtistPostDescription>
          <ArtistPostCommentsViewDivider />
          <ArtistPostCommentsViewCommentsSection>
            {postDetails?.value.parentComments.map((comment) => (
              <PostComment
                key={comment.id}
                author={{
                  photoUrl: comment.authorPhotoUri
                    ? formatCDNImageUrl(comment.authorPhotoUri)
                    : "",
                }}
                content={comment.content}
                date={new Date(comment.createdAt as unknown as string)}
                id={comment.id}
                isLiked={comment.isLikedByCurrentUser}
                likeCount={comment.likesCount}
                postId={postId}
              />
            ))}
          </ArtistPostCommentsViewCommentsSection>
        </ArtistPostContent>
      </ArtistPostScrollingArea>
      <ArtistPostCommentsViewCreateCommentSection>
        <ArtistPostCommentsViewCreateCommentAvatar
          size="sm"
          src={
            me?.foregroundPhotoUri
              ? formatCDNImageUrl(me?.foregroundPhotoUri)
              : ""
          }
        />
        <ArtistPostCommentsViewCreateCommentInput
          placeholder={intl.formatMessage({
            defaultMessage: "Dodaj komentarz...",
            id: "U8IOpL",
          })}
          value={commentContent}
          onChange={(e) => setCommentContent(e.target.value)}
          onKeyDown={(e) => {
            if (e.key === "Enter" && commentContent.length > 0) {
              onCommentSubmitHandler()
            }
          }}
        />
      </ArtistPostCommentsViewCreateCommentSection>
    </ArtistPostCommentsViewWrapper>
  )
}

export default ArtistPostCommentView
