import { Flex } from "@chakra-ui/react"
import type { NextPage } from "next"
import useMe from "../../api/hooks/useMe"
import { BookingRequestStatus } from "../../api/tatuazApi"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
import IncomingBookings from "../../components/dashboard/profile/IncomingBookings"
import MyBookings from "../../components/dashboard/profile/MyBookings"

export type UserProfile = {
  username?: string
  foregroundPhotoUri?: string | null
  backgroundPhotoUri?: string | null
  bio?: string | null
  city?: string | null
  artist?: boolean
}

export const tabIndexToBookingStatus = (index: number) => {
  switch (index) {
    case 0:
      return BookingRequestStatus.Pending
    case 1:
      return BookingRequestStatus.Accepted
    case 2:
      return BookingRequestStatus.Rejected
    default:
      return BookingRequestStatus.Pending
  }
}

const Bookings: NextPage = () => {
  const me = useMe()

  return (
    <DashboardLayout>
      <Flex
        flexWrap="wrap"
        gap={{
          base: 4,
          md: 8,
        }}
        mt={{
          base: 4,
          md: 8,
        }}
      >
        <MyBookings />
        {me?.artist && <IncomingBookings />}
      </Flex>
    </DashboardLayout>
  )
}

export default Bookings
