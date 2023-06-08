import { useMutation } from "@tanstack/react-query"
import { Paragraph } from "@tatuaz/ui"
import { FunctionComponent } from "react"
import { FormattedMessage } from "react-intl"
import { api } from "../../../../../../api/apiClient"
import { queryKeys } from "../../../../../../api/queryKeys"
import { queryClient } from "../../../../../../pages/_app"
import { theme } from "../../../../../../styles/theme"
import {
  CommentOwnerAvatar,
  ArtistPostCommentsViewCommentWrapper,
  CommentContent,
  CommentContentWrapper,
  CommentOptionsWrapper,
  CommentOption,
  CommentReactions,
  ReactionIcon,
} from "./styles"

type PostCommentProps = {
  id: string
  likeCount: number
  postId: string
  isLiked: boolean
  content: string
  date: Date
  author: {
    photoUrl: string
  }
}

const PostComment: FunctionComponent<PostCommentProps> = ({
  id,
  postId,
  likeCount,
  isLiked,
  content,
  author,
  date,
}) => {
  const likeCommentMutation = useMutation({
    mutationFn: (commentId: string) =>
      api.comment.likeComment({
        commentId,
        like: !isLiked,
      }),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [queryKeys.getPostDetails, postId],
      })
    },
    onError: () => {
      queryClient.invalidateQueries({
        queryKey: [queryKeys.getPostDetails, postId],
      })
    },
  })

  return (
    <ArtistPostCommentsViewCommentWrapper>
      <CommentOwnerAvatar size="sm" src={author.photoUrl} />
      <CommentContentWrapper>
        <CommentContent>
          <Paragraph level={2}>{content}</Paragraph>
          {likeCount > 0 && (
            <CommentReactions>
              <ReactionIcon />
              <Paragraph level={2}>
                <FormattedMessage
                  defaultMessage="{likeCount, plural, one {# lubi to} other {# lubią to}}"
                  id="PyyqQv"
                  values={{ likeCount }}
                />
              </Paragraph>
            </CommentReactions>
          )}
        </CommentContent>
        <CommentOptionsWrapper>
          <CommentOption>
            <Paragraph level={2} onClick={() => likeCommentMutation.mutate(id)}>
              {isLiked ? (
                <FormattedMessage defaultMessage="Nie lubię tego" id="MTlDuV" />
              ) : (
                <FormattedMessage defaultMessage="Lubię to" id="k0b45W" />
              )}
            </Paragraph>
          </CommentOption>
          <CommentOption>
            <Paragraph level={2}>
              <FormattedMessage defaultMessage="Odpowiedz" id="mGv3OR" />
            </Paragraph>
          </CommentOption>
          <Paragraph color={theme.colors.background4} level={3}>
            {date.toLocaleDateString("pl-PL", {
              day: "numeric",
              month: "long",
              year: "numeric",
            })}
          </Paragraph>
        </CommentOptionsWrapper>
      </CommentContentWrapper>
    </ArtistPostCommentsViewCommentWrapper>
  )
}

export default PostComment
