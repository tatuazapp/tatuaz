import { Heading } from "@tatuaz/ui"
import {
  UserSectionAreaWrapper,
  UserName,
  UserPhoto,
  UserIcon,
  UserIconStatus,
} from "./styles"

const UserSection = () => (
  <UserSectionAreaWrapper>
    <UserIcon>
      <UserPhoto />
      <UserIconStatus />
    </UserIcon>
    <UserName>
      <Heading level={5}>Richard Brewl</Heading>
    </UserName>
  </UserSectionAreaWrapper>
)

export default UserSection
