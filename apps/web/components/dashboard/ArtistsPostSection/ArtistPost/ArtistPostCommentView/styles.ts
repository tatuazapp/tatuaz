import { HeartFill } from "@styled-icons/bootstrap/HeartFill"
import { CloseOutline } from "@styled-icons/evaicons-outline/CloseOutline"
import { down } from "styled-breakpoints"
import styled from "styled-components"

export const ArtistPostCommentsViewWrapper = styled.div`
  position: relative;
  max-width: 735px;
  max-height: calc(100vh - 110px);
  background-color: ${({ theme }) => theme.colors.background2};

  ${down("md")} {
    height: 100vh;
  }
`

export const ArtistPostScrollingArea = styled.div`
  overflow: scroll;
  max-height: calc(100vh - 200px);
  ::-webkit-scrollbar {
    width: 0;
  }

  /* Track */
  ::-webkit-scrollbar-track {
    background: ${({ theme }) => theme.colors.background1};
  }
`

export const ArtistPostCommentsViewCommentsSection = styled.div`
  display: flex;
  flex-direction: column;
  align-items: flex-start;
`

export const ArtistPostCommentsViewCommentWrapper = styled.div`
  display: flex;
  align-items: flex-start;
  margin-bottom: ${({ theme }) => theme.space.medium};
`

export const CommentOwnerAvatar = styled.div`
  width: ${({ theme }) => theme.space.xxxlarge};
  height: ${({ theme }) => theme.space.xlarge};
  margin-top: ${({ theme }) => theme.space.xxxsmall};
  margin-right: ${({ theme }) => theme.space.xsmall};
  margin-left: ${({ theme }) => theme.space.xsmall};

  /* TODO: change to dynamic */
  background-image: url("https://cdn.benchmark.pl/uploads/article/87749/MODERNICON/49e0c496efa2aedbbb84c1a8ebdbb4b125e1dc33.jpg");
  background-size: cover;
  border-radius: 50%;
`

export const CommentContentWrapper = styled.div`
  display: flex;
  flex-direction: column;
`

export const CommentContent = styled.div`
  position: relative;

  padding-top: ${({ theme }) => theme.space.xxsmall};
  padding-right: ${({ theme }) => theme.space.xsmall};
  padding-bottom: ${({ theme }) => theme.space.xxsmall};
  padding-left: ${({ theme }) => theme.space.xsmall};

  background-color: ${({ theme }) => theme.colors.background3};
  border-radius: ${({ theme }) => theme.radius.small};
`

export const CommentReactions = styled.div`
  position: absolute;
  right: ${({ theme }) => theme.space.xsmall};
  bottom: -${({ theme }) => theme.space.xsmall};

  display: flex;
  align-items: center;

  padding-right: ${({ theme }) => theme.space.xxxsmall};
  padding-left: ${({ theme }) => theme.space.xxxsmall};

  font-size: "10px";

  background-color: ${({ theme }) => theme.colors.background4};
  border-radius: ${({ theme }) => theme.radius.xxsmall};
`

export const ReactionIcon = styled(HeartFill)`
  height: ${({ theme }) => theme.space.small};
  padding-right: ${({ theme }) => theme.space.xxxxsmall};
  color: ${({ theme }) => theme.colors.primary};
`

export const CommentOptionsWrapper = styled.div`
  display: flex;
  align-items: center;
  margin-left: ${({ theme }) => theme.space.xsmall};
`

export const CommentOption = styled.p`
  cursor: pointer;
  margin-right: ${({ theme }) => theme.space.xsmall};
`

export const ArtistPostCommentsViewDivider = styled.div`
  height: 1px;
  margin-top: ${({ theme }) => theme.space.medium};
  margin-bottom: ${({ theme }) => theme.space.medium};
  background-color: ${({ theme }) => theme.colors.background3};
`

export const ArtistPostCommentsViewCreateCommentSection = styled.div`
  position: absolute;
  bottom: 0;

  display: flex;
  align-items: center;

  width: 100%;
  padding-top: ${({ theme }) => theme.space.xsmall};
  padding-bottom: ${({ theme }) => theme.space.xsmall};

  background-color: ${({ theme }) => theme.colors.background1};
  border-top: 1px solid ${({ theme }) => theme.colors.background3};
`

export const ArtistPostCommentsViewCreateCommentAvatar = styled.div`
  display: inline-block;

  box-sizing: content-box;
  width: ${({ theme }) => theme.space.xlarge};
  height: ${({ theme }) => theme.space.xlarge};
  margin: 0 ${({ theme }) => theme.space.xsmall};

  /* TODO: change to dynamic */
  background-image: url("https://cdn.benchmark.pl/uploads/article/87749/MODERNICON/49e0c496efa2aedbbb84c1a8ebdbb4b125e1dc33.jpg");
  background-size: cover;
  border: 2px solid ${({ theme }) => theme.colors.background2};
  border-radius: 50%;
`

export const ArtistPostCommentsViewCreateCommentInput = styled.input`
  width: 100%;
  margin-right: ${({ theme }) => theme.space.xsmall};
  margin-left: ${({ theme }) => theme.space.xsmall};
  padding-top: ${({ theme }) => theme.space.xxsmall};
  padding-bottom: ${({ theme }) => theme.space.xxsmall};
  padding-left: ${({ theme }) => theme.space.xxxsmall};

  color: ${({ theme }) => theme.colors.secondary};

  background-color: ${({ theme }) => theme.colors.background2};
  border: none;
  border: 2px solid ${({ theme }) => theme.colors.background2};
  border-radius: ${({ theme }) => theme.radius.xxsmall};
  outline: none;

  transition: background-color ${({ theme }) => theme.animationTime.xxfast} ease;

  ::placeholder {
    color: ${({ theme }) => theme.colors.background3};
    opacity: 1;
    transition: color ${({ theme }) => theme.animationTime.xxfast} ease;
  }

  :hover {
    background-color: ${({ theme }) => theme.colors.background3};
    border: 2px solid ${({ theme }) => theme.colors.secondary};
    ::placeholder {
      color: ${({ theme }) => theme.colors.background4};
      opacity: 1;
    }
  }
  :focus {
    background-color: ${({ theme }) => theme.colors.background3};
    border: 2px solid ${({ theme }) => theme.colors.secondary};
    ::placeholder {
      color: ${({ theme }) => theme.colors.background4};
      opacity: 1;
    }
  }
`

export const ArtistPostCommentsViewHeader = styled.div`
  position: relative;

  display: flex;
  align-items: center;
  justify-content: center;

  padding-top: ${({ theme }) => theme.space.small};
  padding-bottom: ${({ theme }) => theme.space.small};

  background-color: ${({ theme }) => theme.colors.background1};
  border-bottom: 1px solid ${({ theme }) => theme.colors.background3};
`

export const ArtistPostCommentsViewHeaderCloseButton = styled(CloseOutline)`
  cursor: pointer;

  position: absolute;
  right: ${({ theme }) => theme.space.xsmall};

  height: ${({ theme }) => theme.sizes.xlarge};

  color: ${({ theme }) => theme.colors.primary};
`

export const ArtistPostContent = styled.div`
  padding-top: ${({ theme }) => theme.space.xsmall};
  padding-right: ${({ theme }) => theme.space.xsmall};
  padding-bottom: ${({ theme }) => theme.space.xlarge};
  padding-left: ${({ theme }) => theme.space.xsmall};
`

export const CommentsContainer = styled.div`
  display: flex;
  align-items: center;
`

export const ArtistPostUserWrapper = styled.div`
  display: flex;
  justify-content: start;
  padding-top: ${({ theme }) => theme.space.xsmall};
`
