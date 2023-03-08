import { Paragraph } from "@tatuaz/ui"
import { FormattedMessage } from "react-intl"
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

const PostComment = () => (
  <ArtistPostCommentsViewCommentWrapper>
    <CommentOwnerAvatar />
    <CommentContentWrapper>
      <CommentContent>
        <Paragraph level={2}>
          Besides the goals and assists, all Madrid players won it. Vinicius was
          good. Winning with country matters the most because then you dont
          assemble best players across the world eget est lorem ipsum.
        </Paragraph>
        <CommentReactions>
          <ReactionIcon />
          <Paragraph level={2}>12</Paragraph>
        </CommentReactions>
      </CommentContent>
      <CommentOptionsWrapper>
        <CommentOption>
          <Paragraph level={2}>
            <FormattedMessage defaultMessage="Lubię to" id="k0b45W" />
          </Paragraph>
        </CommentOption>
        <CommentOption>
          <Paragraph level={2}>
            <FormattedMessage defaultMessage="Odpowiedz" id="mGv3OR" />
          </Paragraph>
        </CommentOption>
        <Paragraph color={theme.colors.background4} level={3}>
          <FormattedMessage defaultMessage="Wczoraj" id="M+no1T" />
        </Paragraph>
      </CommentOptionsWrapper>
    </CommentContentWrapper>
  </ArtistPostCommentsViewCommentWrapper>
)

export default PostComment
