import { HeartFill } from "@styled-icons/bootstrap/HeartFill"
import styled from "styled-components"

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
